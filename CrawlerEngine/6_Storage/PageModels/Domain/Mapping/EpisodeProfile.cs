using Crawler.Domain.Models;
using CrawlerEngine._6_Storage.PageModels.Data.Entities;

namespace CrawlerEngine._6_Storage.PageModels.Domain.Mapping
{
    public partial class EpisodeProfile
    {
        public static Episode MapReadModelToEntity(EpisodeReadModel episode)
        {
            var ep = new Episode()
            {
                TitleNo = episode.TitleNo,
                EpisodeName = episode.EpisodeName,
                EpisodeThumbnail = episode.EpisodeThumbnail,
                EpisodeDate = episode.EpisodeDate,
                EpisodeLink = episode.EpisodeLink,
                EpisodeLinkHash = episode.EpisodeLinkHash,
                Updated = episode.Updated,
                ContentHash = episode.ContentHash
            };
            return ep;
        }
        public static Episode MapCreateModelToEntity(EpisodeCreateModel episode)
        {
            var ep = new Episode()
            {
                TitleNo = episode.TitleNo,
                EpisodeName = episode.EpisodeName,
                EpisodeThumbnail = episode.EpisodeThumbnail,
                EpisodeDate = episode.EpisodeDate,
                EpisodeLink = episode.EpisodeLink,
                EpisodeLinkHash = episode.EpisodeLinkHash,
                Updated = episode.Updated,
                ContentHash = episode.ContentHash
            };
            return ep;
        }
        public static Episode MapUpdateModelToEntity(EpisodeUpdateModel episode)
        {
            var ep = new Episode()
            {
                TitleNo = episode.TitleNo,
                EpisodeName = episode.EpisodeName,
                EpisodeThumbnail = episode.EpisodeThumbnail,
                EpisodeDate = episode.EpisodeDate,
                EpisodeLink = episode.EpisodeLink,
                EpisodeLinkHash = episode.EpisodeLinkHash,
                Updated = episode.Updated,
                ContentHash = episode.ContentHash
            };
            return ep;
        }
    }
}
