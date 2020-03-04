using System;
using System.Collections.Generic;

namespace CrawlerEngine.Core._1_Scheduler
{
    public interface IUriBucket<T>:IEnumerable<T>, IDisposable
    {
        void Add(T item);
        void Remove(T item);
        T GetNextUri();
        int Count { get; }
        bool IsEmpty { get; }
    }
}