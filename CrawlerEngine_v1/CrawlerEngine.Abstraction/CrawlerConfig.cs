namespace CrawlerEngine.Abstraction
{
    public class CrawlerConfig : ICrawlerConfig
    {
        public static string ConfigSection => "AppConfig";
        public string TextToPrint { get; set; }
    }
}
