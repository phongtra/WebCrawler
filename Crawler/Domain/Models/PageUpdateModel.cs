using System;
using System.Collections.Generic;

namespace Content.Domain.Models
{
    public partial class PageUpdateModel
    {
        #region Generated Properties
        public long Id { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public long CrawledLinkId { get; set; }

        #endregion

    }
}
