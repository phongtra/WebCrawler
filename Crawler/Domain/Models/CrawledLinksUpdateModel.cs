using System;
using System.Collections.Generic;

namespace Content.Domain.Models
{
    public partial class CrawledLinksUpdateModel
    {
        #region Generated Properties
        public long Id { get; set; }

        public string Url { get; set; }

        public string Retrieved { get; set; }

        public string Updated { get; set; }

        #endregion

    }
}
