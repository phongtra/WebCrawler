using System;
using System.Collections.Generic;
using Crawler.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Content.Data.Mapping
{
    public partial class WebToonMap
        : IEntityTypeConfiguration<WebToon>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WebToon> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("WebToon");

            // key
            builder.HasKey(t => t.TitleNo);

            // properties
            builder.Property(t => t.Uri)
                .IsRequired()
                .HasColumnName("Uri")
                .HasColumnType("TEXT");

            builder.Property(t => t.TitleNo)
                .IsRequired()
                .HasColumnName("TitleNo")
                .HasColumnType("TEXT");

            builder.Property(t => t.ImageLink)
                .IsRequired()
                .HasColumnName("ImageLink")
                .HasColumnType("TEXT");

            builder.Property(t => t.Genre)
                .IsRequired()
                .HasColumnName("Genre")
                .HasColumnType("TEXT");

            builder.Property(t => t.Subject)
                .IsRequired()
                .HasColumnName("Subject")
                .HasColumnType("TEXT");

            builder.Property(t => t.Author)
                .IsRequired()
                .HasColumnName("Author")
                .HasColumnType("TEXT");

            builder.Property(t => t.Updated)
                .HasColumnName("Updated")
                .HasColumnType("TEXT");

            builder.Property(t => t.ContentHash)
                .IsRequired()
                .HasColumnName("ContentHash")
                .HasColumnType("TEXT");

            // relationships
            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "";
        public const string TableName = "WebToon";

        public const string ColumnUri = "Uri";
        public const string ColumnTitleNo = "TitleNo";
        public const string ColumnImageLink = "ImageLink";
        public const string ColumnGenre = "Genre";
        public const string ColumnSubject = "Subject";
        public const string ColumnAuthor = "Author";
        public const string ColumnUpdated = "Updated";
        public const string ColumnContentHash = "ContentHash";
        #endregion
    }
}
