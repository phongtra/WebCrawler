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
        public virtual DbSet<Content.Data.Entities.Episode> Episodes { get; set; }

        public virtual DbSet<Content.Data.Entities.Page> Pages { get; set; }

        public virtual DbSet<Content.Data.Entities.WebToon> WebToons { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new Content.Data.Mapping.EpisodeMap());
            modelBuilder.ApplyConfiguration(new Content.Data.Mapping.PageMap());
            modelBuilder.ApplyConfiguration(new Content.Data.Mapping.WebToonMap());
            #endregion
        }
    }
}
