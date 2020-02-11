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
            builder.Property(t => t.EpisodeLinkHash)
                .IsRequired()
                .HasColumnName("EpisodeLinkHash")
                .HasColumnType("TEXT");

            builder.Property(t => t.Content)
                .IsRequired()
                .HasColumnName("Content")
                .HasColumnType("TEXT");

            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("INTEGER");

            builder.Property(t => t.Updated)
                .HasColumnName("Updated")
                .HasColumnType("TEXT");

            // relationships
            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "";
        public const string TableName = "Page";

        public const string ColumnEpisodeLinkHash = "EpisodeLinkHash";
        public const string ColumnContent = "Content";
        public const string ColumnId = "Id";
        public const string ColumnUpdated = "Updated";
        #endregion
    }
}
