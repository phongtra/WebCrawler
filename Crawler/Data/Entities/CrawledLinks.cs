using System;
using System.Collections.Generic;

namespace Content.Data.Entities
{
    public partial class CrawledLinks
    {
        public CrawledLinks()
        {
            #region Generated Constructor
            CrawledLinkPages = new HashSet<Page>();
            
            #endregion
        }

        #region Generated Properties
        public long Id { get; set; }

        public string Url { get; set; }

        public string Retrieved { get; set; }

        public string Updated { get; set; }

        public string Title { get; set; }

        public string ImageLink { get; set; }

        public string Description { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<Page> CrawledLinkPages { get; set; }

        

        #endregion

    }
}
