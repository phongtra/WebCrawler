using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Content.Data
{
    public partial class ContentContext : DbContext
    {
        public ContentContext(DbContextOptions<ContentContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<Content.Data.Entities.CrawledLinks> CrawledLinks { get; set; }


        public virtual DbSet<Content.Data.Entities.Page> Pages { get; set; }

        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\WebCrawlerPrj\\Crawler\\DB\\content.db;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new Content.Data.Mapping.CrawledLinksMap());
            modelBuilder.ApplyConfiguration(new Content.Data.Mapping.PageMap());
            #endregion
        }
    }
}
