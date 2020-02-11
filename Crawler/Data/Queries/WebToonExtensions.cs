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
        public static Content.Data.Entities.WebToon GetByKey(this IQueryable<Content.Data.Entities.WebToon> queryable, string titleNo)
        {
            if (queryable is DbSet<Content.Data.Entities.WebToon> dbSet)
                return dbSet.Find(titleNo);

            return queryable.FirstOrDefault(q => q.TitleNo == titleNo);
        }

        public static ValueTask<Content.Data.Entities.WebToon> GetByKeyAsync(this IQueryable<Content.Data.Entities.WebToon> queryable, string titleNo)
        {
            if (queryable is DbSet<Content.Data.Entities.WebToon> dbSet)
                return dbSet.FindAsync(titleNo);

            var task = queryable.FirstOrDefaultAsync(q => q.TitleNo == titleNo);
            return new ValueTask<Content.Data.Entities.WebToon>(task);
        }

        #endregion

    }
}
