using Crawler.Domain.Models;
using CrawlerEngine._6_Storage.PageModels.Data.Entities;

namespace CrawlerEngine._6_Storage.PageModels.Domain.Mapping
{
    public partial class WebtoonProfile
    {
        public static WebToon MapReadModelToEntity(WebToonReadModel webToon)
        {
            var web = new WebToon
            {
                Uri = webToon.Uri,
                TitleNo = webToon.TitleNo,
                Subject = webToon.Subject,
                Author = webToon.Author,
                Genre = webToon.Genre,
                ImageLink = webToon.ImageLink,
                ContentHash = webToon.ContentHash,
                Updated = webToon.Updated
            };
            return web;
        }
        public static WebToon MapCreateModelToEntity(WebToonCreateModel webToon)
        {
            var web = new WebToon
            {
                Uri = webToon.Uri,
                TitleNo = webToon.TitleNo,
                Subject = webToon.Subject,
                Author = webToon.Author,
                Genre = webToon.Genre,
                ImageLink = webToon.ImageLink,
                ContentHash = webToon.ContentHash,
                Updated = webToon.Updated
            };
            return web;
        }
        public static WebToon MapUpdateModelToEntity(WebToonUpdateModel webToon)
        {
            var web = new WebToon
            {
                Uri = webToon.Uri,
                TitleNo = webToon.TitleNo,
                Subject = webToon.Subject,
                Author = webToon.Author,
                Genre = webToon.Genre,
                ImageLink = webToon.ImageLink,
                ContentHash = webToon.ContentHash,
                Updated = webToon.Updated
            };
            return web;
        }
    }
}
