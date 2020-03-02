using System.Linq;
using System.Threading.Tasks;
using Crawler.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Data.Queries
{
    public static partial class PageExtensions
    {
        #region Generated Extensions
        public static Page GetByKey(this IQueryable<Page> queryable, long id)
        {
            if (queryable is DbSet<Page> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<Page> GetByKeyAsync(this IQueryable<Page> queryable, long id)
        {
            if (queryable is DbSet<Page> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<Page>(task);
        }

        #endregion

    }
}
