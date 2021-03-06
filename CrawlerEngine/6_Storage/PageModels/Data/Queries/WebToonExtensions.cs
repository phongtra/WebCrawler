using System.Linq;
using System.Threading.Tasks;
using CrawlerEngine._6_Storage.PageModels.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrawlerEngine._6_Storage.PageModels.Data.Queries
{
    public static partial class WebToonExtensions
    {
        #region Generated Extensions
        public static WebToon GetByKey(this IQueryable<WebToon> queryable, string titleNo)
        {
            if (queryable is DbSet<WebToon> dbSet)
                return dbSet.Find(titleNo);

            return queryable.FirstOrDefault(q => q.TitleNo == titleNo);
        }

        public static ValueTask<WebToon> GetByKeyAsync(this IQueryable<WebToon> queryable, string titleNo)
        {
            if (queryable is DbSet<WebToon> dbSet)
                return dbSet.FindAsync(titleNo);

            var task = queryable.FirstOrDefaultAsync(q => q.TitleNo == titleNo);
            return new ValueTask<WebToon>(task);
        }

        #endregion

    }
}
