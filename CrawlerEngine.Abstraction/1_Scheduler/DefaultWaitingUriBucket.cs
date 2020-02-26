﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using UriDB.Data;
using UriDB.Data.Entities;

namespace CrawlerEngine.Abstraction._1_Scheduler
{
    public class DefaultWaitingUriBucket : IUriBucket<WaitingPage>, IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly UriDBContext dbContext;

        public DefaultWaitingUriBucket(IServiceProvider services)
        {
            _scope = services.CreateScope();
            dbContext = _scope.ServiceProvider.GetService<UriDBContext>();
            // dbContext = services.GetService<UriDBContext>();
        }

        IEnumerator<WaitingPage> IEnumerable<WaitingPage>.GetEnumerator()
        {
            return this.GetWaitingPages().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return this.GetWaitingPages().GetEnumerator();
        }

        public async void Add(WaitingPage item)
        {
            // UriToProcess.Enqueue(item);

            item.RequestTime = DateTime.UtcNow.ToString(Tools.StrDateTimeFormat);

            if (item.Id == 0)//new item or need to find item by Hash
            {
                if (item.Priority == null)
                {
                    item.Priority = 0;
                }

                if (item.Verb == null)
                {
                    item.Verb = HttpMethod.Get.ToString();
                }

                item.NeedUpdate = 1;

                item.UriHash = Tools.ComputeSha256Hash(item.Uri + item.Verb.ToLower());

                var pageToFind = FindPageByUriHash(item.UriHash);
                if (pageToFind == null) // new item
                {
                    await dbContext.AddAsync(item);
                }
                else // update already in DB
                {
                    pageToFind.Priority = item.Priority;
                    pageToFind.RequestTime = item.RequestTime;
                    pageToFind.Parameters = item.Parameters;
                    pageToFind.NeedUpdate = item.NeedUpdate;
                    dbContext.Update(pageToFind);
                }
            }
            else
            {
                dbContext.Update(item);
            }
            await dbContext.SaveChangesAsync();
        }

        public async void Remove(WaitingPage item)
        {
            item.NeedUpdate = 0;
            dbContext.Update(item);//Also update any changing in item
            await dbContext.SaveChangesAsync();
        }

        public WaitingPage GetNextUri()
        {
            var waitingPages = GetWaitingPages();

            return waitingPages.FirstOrDefault();
        }

        public int Count
        {
            get
            {
                return GetWaitingPages()
                    .GroupBy(row => new {row.Id})
                    .Select(g => g.Count())
                    .First();
            }
        }

        public bool IsEmpty => Count == 0;

        public void Dispose()
        {
            _scope?.Dispose();
        }

        #region Repository functions

        private IOrderedQueryable<WaitingPage> GetWaitingPages()
        {
            var waitingPages = from waitingPage in dbContext.WaitingPages
                where waitingPage.NeedUpdate == 1
                orderby waitingPage.Priority descending,
                    waitingPage.RequestTime
                select waitingPage;
            return waitingPages;
        }

        private WaitingPage FindPageByUriHash(string uriHash)
        {
            var waitingPages = from waitingPage in dbContext.WaitingPages
                where waitingPage.UriHash == uriHash
                select waitingPage;

            return waitingPages.FirstOrDefault();
        }

        #endregion
    }
}
