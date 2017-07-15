# Algolia Analytics Client for .NET
_built **.NET Standard 1.4**_  
_PS: Please check the dotnet standard support before use.__
## Quickstart
Get popular search for a fixed date period
```csharp
const string apiKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXX";
const string appId = "XXXXXXXXX";

// Create a new client object using the app secrets typically stored away in a safer place than mine :P
var analyticsClient = new AnalyticsClient(new AppSecrets()
{
    ApplicationId = appId,
    ApplicationKey = apiKey
});

// Initialise the new client with the the name of the search index.
analyticsClient.InitAnalytics("JollofIndex");

// Specify the filter options for the request
var options = new AnalyticsOptions()
    .StartAt(DateTime.Today.AddDays(-1))
    .SetCountry("NG")
    .SetSize(10)
    .EndAt(DateTime.Now);

// Pass in the options
// ...and the analytics data you want
var popularSearches = await analyticsClient.GetAnalyticsForSearch(options,
    AnalyticsClient.AnalyticSearchType.Popular);
```

## Usage  
The [Algolia Analytics API Documentation](https://www.algolia.com/doc/rest-api/analytics/) basically provides two kinds of endpoints. One for Searches and another for Rate Limits. So it makes sense to separate these in their individual calling methods as well.   

AnalyticsClient - All new request starts with this. You'd typically have to create a new AnalyticsClient object and pass in your AppSecrets like this `var client = new AnalyticsClient(<AppSecrets> secrets);` You can henceforth use the `client` for all your operations.  

AppSecrets - This has two properties `ApplicationId` and `ApplicationKey` and they are exactly as their name suggests. Please visit your Algolia Dashboard to get both of these.  

AnalyticsOptions - Gives you the ability to set filters for your request. These options were created to match the parameters of the REST API provided by Algolia. Methods include:  
*  `SetSize(int size)`
*  `StartAt(DateTime startTime)`
*  `EndAt(DateTime EndTime)`
*  `SetTags(IEnumerable<string> tags)`
*  `SetCountry(string country)`
*  `SetRefinements(bool refiments)`  
See [Algolia Analytics API Documentation](https://www.algolia.com/doc/rest-api/analytics/) for more information.

InitAnalytics - Used to set the index or indices that would be queried.
`InitAnalytics(<string> Index);` or  `InitAnalytics(<IEnumerable<string>> Indices);`

### Getting Analytics for Search
AnalyticsSearchType - Specifies what the type of analytics for search should be. These resource endpoints were created to match the different queries of the REST API provided by Algolia.
*  `AnalyticsSearchType.Popular`
*  `AnalyticsSearchType.NoResults`
*  `AnalyticsSearchType.Countries`
*  `AnalyticsSearchType.Hits`
*  `AnalyticsSearchType.HitsWithTypo`
*  `AnalyticsSearchType.TopIps`
*  `AnalyticsSearchType.Referers`  
See [Algolia Analytics API Documentation](https://www.algolia.com/doc/rest-api/analytics/) for more information.

GetAnalyticsForSearch - GetAnalyticsForSearch([<AnalyticsOptions> options], <AnalyticsSearchType> searchType) - Asynchronous method. Takes an optional AnalyticsOptions object and an AnalyticsSearchType enum.
```csharp
var popularSearches = await GetAnalyticsSearch(AnalyticsClient.AnalyticSearchType.Popular);
/// OR 
var options = new AnalyticsOptions();
options.SetCountry("NG")
    .SetRefinements(true);
var popularSearches = await GetAnalyticsSearch(options, AnalyticsClient.AnalyticSearchType.Popular);
```

### Getting Analytics for RateLimit
RateLimitSearchType -  Specifies what the type of analytics for ratelimits should be. These resource endpoints were created to match the different queries of the REST API provided by Algolia.
*  AnalyticRateLimitType.TopIps`  
See [Algolia Analytics API Documentation](https://www.algolia.com/doc/rest-api/analytics/) for more information.

GetAnalyticsForRateLimits - GetAnalyticsForRateLimits([<AnalyticsOptions> options], <AnalyticRateLimitType> rateLimitTypes) - Asynchronous method. Takes an optional AnalyticsOptions object and an AnalyticRateLimitType enum.
```csharp
var rateLimits = await AnalyticRateLimitType(AnalyticsClient.AnalyticSearchType.Popular);
// OR 
var options = new AnalyticsOptions();
options.SetCountry("NG")
    .SetRefinements(true);
var rateLimits = await AnalyticRateLimitType(options, AnalyticsClient.AnalyticRateLimitType.TopIps);
```
