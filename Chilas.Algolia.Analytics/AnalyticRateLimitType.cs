namespace Algolia.Analytics
{
    public partial class AnalyticsClient
    {
        public enum AnalyticRateLimitType
        {
            TopIps
        }

        /// <summary>
        /// Gets the parameter for the resource endpoint based on the searchtype selected
        /// </summary>
        /// <param name="rateLimitType"></param>
        /// <returns></returns>
        private static string GetSearchResource(AnalyticRateLimitType rateLimitType)
        {
            switch (rateLimitType)
            {
                case AnalyticRateLimitType.TopIps:
                    return "ips";
                default:
                    throw new AlgoliaException("Invalid Rate Limit Feature Selected");
            }
        }
    }
}