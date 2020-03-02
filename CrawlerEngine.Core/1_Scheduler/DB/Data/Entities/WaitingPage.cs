namespace CrawlerEngine.Core._1_Scheduler.DB.Data.Entities
{
    public partial class WaitingPage
    {
        public WaitingPage()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public long Id { get; set; }

        public string Uri { get; set; }

        public string UriHash { get; set; }

        public double? Priority { get; set; }

        public string RequestTime { get; set; }

        public string Verb { get; set; }

        public string DownloadedTime { get; set; }

        public string Parameters { get; set; }

        public long? NeedUpdate { get; set; }

        public string RetrieveErrorAtStep { get; set; }

        public string Error { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
