using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Content.Data.Queries
{
    public static partial class EpisodeExtensions
    {
        #region Generated Extensions
        public static Content.Data.Entities.Episode GetByKey(this IQueryable<Content.Data.Entities.Episode> queryable, string episodeLinkHash)
        {
            if (queryable is DbSet<Content.Data.Entities.Episode> dbSet)
                return dbSet.Find(episodeLinkHash);

            return queryable.FirstOrDefault(q => q.EpisodeLinkHash == episodeLinkHash);
        }

        public static ValueTask<Content.Data.Entities.Episode> GetByKeyAsync(this IQueryable<Content.Data.Entities.Episode> queryable, string episodeLinkHash)
        {
            if (queryable is DbSet<Content.Data.Entities.Episode> dbSet)
                return dbSet.FindAsync(episodeLinkHash);

            var task = queryable.FirstOrDefaultAsync(q => q.EpisodeLinkHash == episodeLinkHash);
            return new ValueTask<Content.Data.Entities.Episode>(task);
        }

        #endregion

    }
}
