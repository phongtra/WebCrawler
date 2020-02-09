namespace Content.Domain.Mapping
{
    public partial class PageProfile
    {
        public static Data.Entities.Page MapReadModelToEntity(Models.PageReadModel pageModel)
        {
            return new Data.Entities.Page()
            {
                Id = pageModel.Id,
                Url = pageModel.Url,
                Content = pageModel.Content,
                CrawledLinkId = pageModel.CrawledLinkId
            };
        }

        public static Data.Entities.Page MapCreateModelToEntity(Models.PageCreateModel pageModel)
        {
            return new Data.Entities.Page()
            {
                Url = pageModel.Url,
                Content = pageModel.Content,
                CrawledLinkId = pageModel.CrawledLinkId
            };
        }

        public static Data.Entities.Page MapUpdateModelToEntity(Models.PageUpdateModel pageModel)
        {
            return new Data.Entities.Page()
            {
                Id = pageModel.Id,
                Url = pageModel.Url,
                Content = pageModel.Content,
                CrawledLinkId = pageModel.CrawledLinkId
            };
        }
    }
}
