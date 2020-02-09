using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Content.Data.Mapping
{
    public partial class PageMap
        : IEntityTypeConfiguration<Content.Data.Entities.Page>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Content.Data.Entities.Page> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Page");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("ID")
                .HasColumnType("INTEGER");

            builder.Property(t => t.Url)
                .IsRequired()
                .HasColumnName("Url")
                .HasColumnType("TEXT");

            builder.Property(t => t.Content)
                .HasColumnName("Content")
                .HasColumnType("TEXT");

            builder.Property(t => t.CrawledLinkId)
                .IsRequired()
                .HasColumnName("CrawledLinkId")
                .HasColumnType("INTEGER");

            // relationships
            builder.HasOne(t => t.CrawledLinkCrawledLinks)
                .WithMany(t => t.CrawledLinkPages)
                .HasForeignKey(d => d.CrawledLinkId)
                .HasConstraintName("2");

            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "";
        public const string TableName = "Page";

        public const string ColumnId = "ID";
        public const string ColumnUrl = "Url";
        public const string ColumnContent = "Content";
        public const string ColumnCrawledLinkId = "CrawledLinkId";
        #endregion
    }
}
