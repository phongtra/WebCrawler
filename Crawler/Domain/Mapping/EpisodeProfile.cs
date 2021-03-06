﻿using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Data.Entities;
using Crawler.Domain.Models;

namespace Crawler.Domain.Mapping
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
