using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UriDB.Data.Mapping
{
    public partial class WaitingPageMap
        : IEntityTypeConfiguration<UriDB.Data.Entities.WaitingPage>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UriDB.Data.Entities.WaitingPage> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("WaitingPage");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("INTEGER");

            builder.Property(t => t.Uri)
                .HasColumnName("Uri")
                .HasColumnType("TEXT");

            builder.Property(t => t.UriHash)
                .HasColumnName("UriHash")
                .HasColumnType("TEXT");

            builder.Property(t => t.Priority)
                .HasColumnName("Priority")
                .HasColumnType("REAL");

            builder.Property(t => t.RequestTime)
                .HasColumnName("RequestTime")
                .HasColumnType("TEXT");

            builder.Property(t => t.Verb)
                .HasColumnName("Verb")
                .HasColumnType("TEXT");

            builder.Property(t => t.DownloadedTime)
                .HasColumnName("DownloadedTime")
                .HasColumnType("TEXT");

            builder.Property(t => t.Parameters)
                .HasColumnName("Parameters")
                .HasColumnType("TEXT");

            builder.Property(t => t.NeedUpdate)
                .HasColumnName("NeedUpdate")
                .HasColumnType("INTEGER");

            // relationships
            #endregion
        }

        #region Generated Constants
        public const string TableSchema = "";
        public const string TableName = "WaitingPage";

        public const string ColumnId = "Id";
        public const string ColumnUri = "Uri";
        public const string ColumnUriHash = "UriHash";
        public const string ColumnPriority = "Priority";
        public const string ColumnRequestTime = "RequestTime";
        public const string ColumnVerb = "Verb";
        public const string ColumnDownloadedTime = "DownloadedTime";
        public const string ColumnParameters = "Parameters";
        public const string ColumnNeedUpdate = "NeedUpdate";
        #endregion
    }
}
