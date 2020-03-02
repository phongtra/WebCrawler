using CrawlerEngine.Core._1_Scheduler.DB.Data.Entities;
using CrawlerEngine.Core._1_Scheduler.DB.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CrawlerEngine.Core._1_Scheduler.DB.Data
{
    public partial class UriDBContext : DbContext
    {
        public UriDBContext(DbContextOptions<UriDBContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<WaitingPage> WaitingPages { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new WaitingPageMap());
            #endregion
        }
    }
}
