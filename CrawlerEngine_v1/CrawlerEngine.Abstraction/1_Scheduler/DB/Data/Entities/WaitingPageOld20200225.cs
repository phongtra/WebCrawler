using System;
using System.Collections.Generic;

namespace UriDB.Data.Entities
{
    public partial class WaitingPageOld20200225
    {
        public WaitingPageOld20200225()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public long Id { get; set; }

        public string Uri { get; set; }

        public string UriHash { get; set; }

        public long? Priority { get; set; }

        public string RequestTime { get; set; }

        public string Verb { get; set; }

        public string DownloadedTime { get; set; }

        public string Parameters { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
