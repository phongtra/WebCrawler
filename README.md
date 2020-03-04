WebCrawlerPrj

**To run**

To run the project, ReactServer, CrawlerDisplayAPI, and ComicAPIGateway needs to run

**important** to set the correct connection string to display to the website

**1** cd `CrawlerDisplayAPI`

**2** nano appsettings.json

**3** Change the connection string to content.db, which is located in Crawler/DB/content.db

**kind of important** Set connection string so that Crawler can persist to the correct DB

**1** cd `Crawler`

**2** nano Startup.cs

**3** At the part  
		`services.AddDbContext<ContentContext>(options =>
            {
                options.UseSqlite(@"Data Source=/home/phong/WebCrawlerPrj/Crawler/DB/content.db;");
            });
		`
Change the connection string to the exact path to Crawler/DB/content.db

**To start website**

**1**. ReactServer: Navigate to ReactServer folder and type in `dotnet run`

**2**. CrawlerDisplayAPI: Navigate to CrawlerDisplayAPI and type in `dotnet run`

**3**. ComicAPIGateway: Navigate to ComicAPIGateway and type in `dotnet run`

**4**. The application should be available on port 2000

