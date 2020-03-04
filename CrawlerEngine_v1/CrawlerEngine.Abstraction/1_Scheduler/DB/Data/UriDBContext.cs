using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UriDB.Data
{
    public partial class UriDBContext : DbContext
    {
        public UriDBContext(DbContextOptions<UriDBContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<UriDB.Data.Entities.WaitingPage> WaitingPages { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new UriDB.Data.Mapping.WaitingPageMap());
            #endregion
        }
    }
}
