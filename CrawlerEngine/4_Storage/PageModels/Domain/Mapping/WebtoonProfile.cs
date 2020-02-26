using System;
using System.Collections.Generic;
using System.Text;
using Crawler.Data.Entities;
using Crawler.Domain.Models;

namespace Crawler.Domain.Mapping
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
