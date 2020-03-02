using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UriDB.Data.Queries
{
    public static partial class WaitingPageExtensions
    {
        #region Generated Extensions
        public static UriDB.Data.Entities.WaitingPage GetByKey(this IQueryable<UriDB.Data.Entities.WaitingPage> queryable, long id)
        {
            if (queryable is DbSet<UriDB.Data.Entities.WaitingPage> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<UriDB.Data.Entities.WaitingPage> GetByKeyAsync(this IQueryable<UriDB.Data.Entities.WaitingPage> queryable, long id)
        {
            if (queryable is DbSet<UriDB.Data.Entities.WaitingPage> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<UriDB.Data.Entities.WaitingPage>(task);
        }

        #endregion

    }
}
