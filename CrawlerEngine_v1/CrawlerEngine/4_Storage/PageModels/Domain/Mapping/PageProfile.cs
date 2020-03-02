using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Data.Entities;
using Crawler.Domain.Models;

namespace Crawler.Domain.Mapping
{
    public partial class PageProfile
    {
        public static Page MapReadModelToEntity(PageReadModel page)
        {
            var pag = new Page()
            {
                Id = page.Id,
                Content = page.Content,
                EpisodeLinkHash = page.EpisodeLinkHash,
                Updated = page.Updated
            };
            return pag;
        }
        public static Page MapCreateModelToEntity(PageCreateModel page)
        {
            var pag = new Page()
            {
                Content = page.Content,
                EpisodeLinkHash = page.EpisodeLinkHash,
                Updated = page.Updated
            };
            return pag;
        }
        public static Page MapUpdateModelToEntity(PageUpdateModel page)
        {
            var pag = new Page()
            {
                Id = page.Id,
                Content = page.Content,
                EpisodeLinkHash = page.EpisodeLinkHash,
                Updated = page.Updated
            };
            return pag;
        }
    }
}
