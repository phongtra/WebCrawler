using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CrawlerEngine.Abstraction.Pooling
{
    public class Pool<T> : IDisposable
    {
        private bool isDisposed;
        private Func<Pool<T>, T> factory;
        private LoadingMode loadingMode;
        private IItemStore itemStore;
        private int size;
        private int count;
        private Semaphore sync;

        public Pool(int size, Func<Pool<T>, T> factory)
            : this(size, factory, LoadingMode.Lazy, AccessMode.FIFO)
        {
        }

        public Pool(int size, Func<Pool<T>, T> factory,
            LoadingMode loadingMode, AccessMode accessMode)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException("size", size,
                    "Argument 'size' must be greater than zero.");
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.size        = size;
            this.factory     = factory;
            sync             = new Semaphore(size, size);
            this.loadingMode = loadingMode;
            this.itemStore   = CreateItemStore(accessMode);
            if (loadingMode == LoadingMode.Eager)
            {
                PreloadItems();
            }
        }

        public T Acquire()
        {
            sync.WaitOne();
            switch (loadingMode)
            {
                case LoadingMode.Eager:
                    return AcquireEager();
                case LoadingMode.Lazy:
                    return AcquireLazy();
                default:
                    Debug.Assert(loadingMode == LoadingMode.LazyExpanding,
                        "Unknown LoadingMode encountered in Acquire method.");
                    return AcquireLazyExpanding();
            }
        }

        public void Release(T item)
        {
            lock (itemStore)
            {
                itemStore.Store(item);
            }
            sync.Release();
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            isDisposed = true;
            if (typeof(IDisposable).IsAssignableFrom(typeof(T)))
            {
                lock (itemStore)
                {
                    while (itemStore.Count > 0)
                    {
                        IDisposable disposable = (IDisposable)itemStore.Fetch();
                        disposable.Dispose();
                    }
                }
            }
            sync.Close();
        }

        #region Acquisition

        private T AcquireEager()
        {
            lock (itemStore)
            {
                return itemStore.Fetch();
            }
        }

        private T AcquireLazy()
        {
            lock (itemStore)
            {
                if (itemStore.Count > 0)
                {
                    return itemStore.Fetch();
                }
            }
            Interlocked.Increment(ref count);
            return factory(this);
        }

        private T AcquireLazyExpanding()
        {
            bool shouldExpand = false;
            if (count < size)
            {
                int newCount = Interlocked.Increment(ref count);
                if (newCount <= size)
                {
                    shouldExpand = true;
                }
                else
                {
                    // Another thread took the last spot - use the store instead
                    Interlocked.Decrement(ref count);
                }
            }
            if (shouldExpand)
            {
                return factory(this);
            }
            else
            {
                lock (itemStore)
                {
                    return itemStore.Fetch();
                }
            }
        }

        private void PreloadItems()
        {
            for (int i = 0; i < size; i++)
            {
                T item = factory(this);
                itemStore.Store(item);
            }
            count = size;
        }

        #endregion

        #region Collection Wrappers

        interface IItemStore
        {
            T    Fetch();
            void Store(T item);
            int  Count { get; }
        }

        private IItemStore CreateItemStore(AccessMode mode)
        {
            switch (mode)
            {
                case AccessMode.FIFO:
                    return new QueueStore();
                case AccessMode.LIFO:
                    return new StackStore();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        class QueueStore : ConcurrentQueue<T>, IItemStore
        {
            public T Fetch()
            {
                return TryDequeue(out var result) ? result : default;
            }

            public void Store(T item)
            {
                Enqueue(item);
            }
        }

        class StackStore : ConcurrentStack<T>, IItemStore
        {

            public T Fetch()
            {
                return TryPop(out var result) ? result : default;
            }

            public void Store(T item)
            {
                Push(item);
            }
        }


        #endregion

        public bool IsDisposed
        {
            get { return isDisposed; }
        }
    }
}