namespace Algolia.Analytics
{
    public partial class AnalyticsClient
    {
        public enum AnalyticSearchType
        {
            Popular,
            NoResults,
            Countries,
            Hits,
            HitsWithTypo,
            TopIps,
            Referers
        }

        /// <summary>
        /// Gets the parameter for the resource endpoint based on the searchtype selected
        /// </summary>
        /// <param name="searchType"></param>
        /// <returns></returns>
        private static string GetSearchResource(AnalyticSearchType searchType)
        {
            switch (searchType)
            {
                case AnalyticSearchType.Popular:
                    return "popular";
                case AnalyticSearchType.NoResults:
                    return "noresults";
                case AnalyticSearchType.Countries:
                    return "countries";
                case AnalyticSearchType.Hits:
                    return "hits";
                case AnalyticSearchType.HitsWithTypo:
                    return "hitswithtypo";
                case AnalyticSearchType.TopIps:
                    return "ips";
                case AnalyticSearchType.Referers:
                    return "referer";
                default:
                    throw new AlgoliaException("Invalid Search Feature Selected");
            }
        }
    }
}