using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Content.Data.Mapping
{
    public partial class WebToonMap
        : IEntityTypeConfiguration<Content.Data.Entities.WebToon>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Content.Data.Entities.WebToon> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("WebToon");

            // key
            builder.HasKey(t => t.UriHash);

            // properties
            builder.Property(t => t.Uri)
                .IsRequired()
                .HasColumnName("Uri")
                .HasColumnType("TEXT");

            builder.Property(t => t.UriHash)
                .IsRequired()
                .HasColumnName("UriHash")
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
                .HasColumnName("ContentHash")
                .HasColumnType("TEXT");

            // relationships
            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "";
        public const string TableName = "WebToon";

        public const string ColumnUri = "Uri";
        public const string ColumnUriHash = "UriHash";
        public const string ColumnImageLink = "ImageLink";
        public const string ColumnGenre = "Genre";
        public const string ColumnSubject = "Subject";
        public const string ColumnAuthor = "Author";
        public const string ColumnUpdated = "Updated";
        public const string ColumnContentHash = "ContentHash";
        #endregion
    }
}
