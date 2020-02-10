using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Content.Data.Queries
{
    public static partial class WebToonExtensions
    {
        #region Generated Extensions
        public static Content.Data.Entities.WebToon GetByKey(this IQueryable<Content.Data.Entities.WebToon> queryable, string uriHash)
        {
            if (queryable is DbSet<Content.Data.Entities.WebToon> dbSet)
                return dbSet.Find(uriHash);

            return queryable.FirstOrDefault(q => q.UriHash == uriHash);
        }

        public static ValueTask<Content.Data.Entities.WebToon> GetByKeyAsync(this IQueryable<Content.Data.Entities.WebToon> queryable, string uriHash)
        {
            if (queryable is DbSet<Content.Data.Entities.WebToon> dbSet)
                return dbSet.FindAsync(uriHash);

            var task = queryable.FirstOrDefaultAsync(q => q.UriHash == uriHash);
            return new ValueTask<Content.Data.Entities.WebToon>(task);
        }

        #endregion

    }
}
