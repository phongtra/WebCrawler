using System.Linq;
using System.Threading.Tasks;
using CrawlerEngine._4_Storage.PageModels.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrawlerEngine._4_Storage.PageModels.Data.Queries
{
    public static partial class EpisodeExtensions
    {
        #region Generated Extensions
        public static Episode GetByKey(this IQueryable<Episode> queryable, string episodeLinkHash)
        {
            if (queryable is DbSet<Episode> dbSet)
                return dbSet.Find(episodeLinkHash);

            return queryable.FirstOrDefault(q => q.EpisodeLinkHash == episodeLinkHash);
        }

        public static ValueTask<Episode> GetByKeyAsync(this IQueryable<Episode> queryable, string episodeLinkHash)
        {
            if (queryable is DbSet<Episode> dbSet)
                return dbSet.FindAsync(episodeLinkHash);

            var task = queryable.FirstOrDefaultAsync(q => q.EpisodeLinkHash == episodeLinkHash);
            return new ValueTask<Episode>(task);
        }

        #endregion

    }
}
