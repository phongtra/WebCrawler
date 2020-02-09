using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Content.Data.Queries
{
    public static partial class PageExtensions
    {
        #region Generated Extensions
        public static IQueryable<Content.Data.Entities.Page> ByCrawledLinkId(this IQueryable<Content.Data.Entities.Page> queryable, long crawledLinkId)
        {
            return queryable.Where(q => q.CrawledLinkId == crawledLinkId);
        }

        public static Content.Data.Entities.Page GetByKey(this IQueryable<Content.Data.Entities.Page> queryable, long id)
        {
            if (queryable is DbSet<Content.Data.Entities.Page> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Content.Data.Entities.Page> GetByKeyAsync(this IQueryable<Content.Data.Entities.Page> queryable, long id)
        {
            if (queryable is DbSet<Content.Data.Entities.Page> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Content.Data.Entities.Page>(task);
        }

        #endregion

    }
}
