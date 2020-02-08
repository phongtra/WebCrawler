using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Content.Data.Mapping
{
    public partial class CrawledLinksMap
        : IEntityTypeConfiguration<Content.Data.Entities.CrawledLinks>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Content.Data.Entities.CrawledLinks> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("CrawledLinks");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("INTEGER");

            builder.Property(t => t.Url)
                .IsRequired()
                .HasColumnName("Url")
                .HasColumnType("TEXT");

            builder.Property(t => t.Retrieved)
                .HasColumnName("Retrieved")
                .HasColumnType("TEXT");

            builder.Property(t => t.Updated)
                .HasColumnName("Updated")
                .HasColumnType("TEXT");

            // relationships
            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "";
        public const string TableName = "CrawledLinks";

        public const string ColumnId = "Id";
        public const string ColumnUrl = "Url";
        public const string ColumnRetrieved = "Retrieved";
        public const string ColumnUpdated = "Updated";
        #endregion
    }
}
