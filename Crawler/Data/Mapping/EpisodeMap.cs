using Crawler.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Data.Mapping
{
    public partial class EpisodeMap
        : IEntityTypeConfiguration<Episode>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Episode> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Episode");

            // key
            builder.HasKey(t => t.EpisodeLinkHash);

            // properties
            builder.Property(t => t.TitleNo)
                .IsRequired()
                .HasColumnName("TitleNo")
                .HasColumnType("TEXT");

            builder.Property(t => t.EpisodeName)
                .IsRequired()
                .HasColumnName("EpisodeName")
                .HasColumnType("TEXT");

            builder.Property(t => t.EpisodeThumbnail)
                .IsRequired()
                .HasColumnName("EpisodeThumbnail")
                .HasColumnType("TEXT");

            builder.Property(t => t.EpisodeDate)
                .IsRequired()
                .HasColumnName("EpisodeDate")
                .HasColumnType("TEXT");

            builder.Property(t => t.EpisodeLink)
                .IsRequired()
                .HasColumnName("EpisodeLink")
                .HasColumnType("TEXT");

            builder.Property(t => t.EpisodeLinkHash)
                .IsRequired()
                .HasColumnName("EpisodeLinkHash")
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
        public const string TableName = "Episode";

        public const string ColumnTitleNo = "TitleNo";
        public const string ColumnEpisodeName = "EpisodeName";
        public const string ColumnEpisodeThumbnail = "EpisodeThumbnail";
        public const string ColumnEpisodeDate = "EpisodeDate";
        public const string ColumnEpisodeLink = "EpisodeLink";
        public const string ColumnEpisodeLinkHash = "EpisodeLinkHash";
        public const string ColumnUpdated = "Updated";
        public const string ColumnContentHash = "ContentHash";
        #endregion
    }
}
