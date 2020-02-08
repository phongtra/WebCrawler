using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Content.Data.Queries
{
    public static partial class CrawledLinksExtensions
    {
        #region Generated Extensions
        public static Content.Data.Entities.CrawledLinks GetByKey(this IQueryable<Content.Data.Entities.CrawledLinks> queryable, long id)
        {
            if (queryable is DbSet<Content.Data.Entities.CrawledLinks> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Content.Data.Entities.CrawledLinks> GetByKeyAsync(this IQueryable<Content.Data.Entities.CrawledLinks> queryable, long id)
        {
            if (queryable is DbSet<Content.Data.Entities.CrawledLinks> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Content.Data.Entities.CrawledLinks>(task);
        }

        #endregion

    }
}
