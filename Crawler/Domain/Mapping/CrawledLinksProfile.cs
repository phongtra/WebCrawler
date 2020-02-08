using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Domain.Mapping
{
    public partial class CrawledLinksProfile
    {
        public static Content.Data.Entities.CrawledLinks MapReadModelToEntity(Content.Domain.Models.CrawledLinksReadModel crawledLinks)
        {
            return new Content.Data.Entities.CrawledLinks()
            {
                Id = crawledLinks.Id,
                Url = crawledLinks.Url,
                Retrieved = crawledLinks.Retrieved,
                Updated = crawledLinks.Updated
            };
        }
        public static Content.Data.Entities.CrawledLinks MapCreateModelToEntity(Content.Domain.Models.CrawledLinksCreateModel crawledLinks)
        {
            return new Content.Data.Entities.CrawledLinks()
            {
                Url = crawledLinks.Url,
                Retrieved = crawledLinks.Retrieved,
                Updated = crawledLinks.Updated
            };
        }
        public static Content.Data.Entities.CrawledLinks MapUpdateModelToEntity(Content.Domain.Models.CrawledLinksUpdateModel crawledLinks)
        {
            return new Content.Data.Entities.CrawledLinks()
            {
                Id = crawledLinks.Id,
                Url = crawledLinks.Url,
                Retrieved = crawledLinks.Retrieved,
                Updated = crawledLinks.Updated
            };
        }

    }
}
