using Crawler.Domain.Models;
using CrawlerEngine._6_Storage.PageModels.Data.Entities;

namespace CrawlerEngine._6_Storage.PageModels.Domain.Mapping
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
