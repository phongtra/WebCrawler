using System;
using System.Threading;

namespace CrawlerEngine.Abstraction.Pooling
{
    public interface IFoo : IDisposable
    {
        void Test();
    }

    public class Foo : IFoo
    {
        private static int count = 0;

        private int num;

        public Foo()
        {
            num = Interlocked.Increment(ref count);
        }

        public void Dispose()
        {
            Console.WriteLine("Goodbye from Foo #{0}", num);
        }

        public void Test()
        {
            Console.WriteLine("Hello from Foo #{0}", num);
        }
    }

    public class PooledFoo : IFoo
    {
        private Foo internalFoo;
        private Pool<IFoo> pool;

        public PooledFoo(Pool<IFoo> pool)
        {
            if (pool == null)
                throw new ArgumentNullException("pool");

            this.pool = pool;
            this.internalFoo = new Foo();
        }

        public void Dispose()
        {
            if (pool.IsDisposed)
            {
                internalFoo.Dispose();
            }
            else
            {
                pool.Release(this);
            }
        }

        public void Test()
        {
            internalFoo.Test();
        }
    }
}