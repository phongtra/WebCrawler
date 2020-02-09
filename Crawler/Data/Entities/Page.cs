using System;
using System.Collections.Generic;

namespace Content.Data.Entities
{
    public partial class Page
    {
        public Page()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public long Id { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public long CrawledLinkId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual CrawledLinks CrawledLinkCrawledLinks { get; set; }

        #endregion

    }
}
