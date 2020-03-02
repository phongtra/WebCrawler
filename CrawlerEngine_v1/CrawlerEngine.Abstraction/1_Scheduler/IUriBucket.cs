using System;
using System.Collections;
using System.Collections.Generic;
using UriDB.Data.Entities;

namespace CrawlerEngine.Abstraction._1_Scheduler
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