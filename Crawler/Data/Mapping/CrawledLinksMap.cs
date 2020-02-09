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

            builder.Property(t => t.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("TEXT");

            builder.Property(t => t.ImageLink)
                .HasColumnName("ImageLink")
                .HasColumnType("TEXT");

            builder.Property(t => t.Description)
                .HasColumnName("Description")
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
        public const string ColumnTitle = "Title";
        public const string ColumnImageLink = "ImageLink";
        public const string ColumnDescription = "Description";
        #endregion
    }
}
