using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UriDB.Data.Queries
{
    public static partial class WaitingPageOld20200225Extensions
    {
        #region Generated Extensions
        public static UriDB.Data.Entities.WaitingPageOld20200225 GetByKey(this IQueryable<UriDB.Data.Entities.WaitingPageOld20200225> queryable, long id)
        {
            if (queryable is DbSet<UriDB.Data.Entities.WaitingPageOld20200225> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<UriDB.Data.Entities.WaitingPageOld20200225> GetByKeyAsync(this IQueryable<UriDB.Data.Entities.WaitingPageOld20200225> queryable, long id)
        {
            if (queryable is DbSet<UriDB.Data.Entities.WaitingPageOld20200225> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<UriDB.Data.Entities.WaitingPageOld20200225>(task);
        }

        #endregion

    }
}
