using Crawler.Data.Entities;
using Crawler.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Data
{
    public partial class ContentContext : DbContext
    {
        public ContentContext(DbContextOptions<ContentContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<Episode> Episodes { get; set; }

        public virtual DbSet<Page> Pages { get; set; }

        public virtual DbSet<WebToon> WebToons { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new EpisodeMap());
            modelBuilder.ApplyConfiguration(new PageMap());
            modelBuilder.ApplyConfiguration(new WebToonMap());
            #endregion
        }
    }
}
