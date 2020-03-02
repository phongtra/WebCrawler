using CrawlerEngine._6_Storage.PageModels.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrawlerEngine._6_Storage.PageModels.Data.Mapping
{
    public partial class PageMap
        : IEntityTypeConfiguration<Page>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Page> builder)
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

            builder.Property(t => t.EpisodeLink)
                .HasColumnName("EpisodeLink")
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
        public const string ColumnEpisodeLink = "EpisodeLink";
        #endregion
    }
}
