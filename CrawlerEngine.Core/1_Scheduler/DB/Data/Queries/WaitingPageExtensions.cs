using System.Linq;
using System.Threading.Tasks;
using CrawlerEngine.Core._1_Scheduler.DB.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrawlerEngine.Core._1_Scheduler.DB.Data.Queries
{
    public static partial class WaitingPageExtensions
    {
        #region Generated Extensions
        public static WaitingPage GetByKey(this IQueryable<WaitingPage> queryable, long id)
        {
            if (queryable is DbSet<WaitingPage> dbSet)
                return dbSet.Find(id);

            return queryable.FirstOrDefault(q => q.Id == id);
        }

        public static ValueTask<WaitingPage> GetByKeyAsync(this IQueryable<WaitingPage> queryable, long id)
        {
            if (queryable is DbSet<WaitingPage> dbSet)
                return dbSet.FindAsync(id);

            var task = queryable.FirstOrDefaultAsync(q => q.Id == id);
            return new ValueTask<WaitingPage>(task);
        }

        #endregion

    }
}
