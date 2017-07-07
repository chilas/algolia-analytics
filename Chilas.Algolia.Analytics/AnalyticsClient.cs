using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Algolia.Analytics
{
    public partial class AnalyticsClient
    {
        private readonly HttpClient _httpClient;
        private string Indices { get; set; } = "";
        private string AnalyticsApiUrl { get; set; } = "https://analytics.algolia.com/1";
        public AnalyticsClient(AppSecrets secrets)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Algolia-API-KEY", secrets.ApplicationKey);
            _httpClient.DefaultRequestHeaders.Add("X-Algolia-Application-Id", secrets.ApplicationId);
        }

        /// <summary>
        /// Initialises the Analytics client with multiple indices
        /// </summary>
        /// <param name="indices"></param>
        public void InitAnalytics(IEnumerable<string> indices)
        {
            var indicesString = string.Empty;
            indicesString = indices.Aggregate(indicesString, (current, next) => string.IsNullOrEmpty(current) ? next : $"{current},{next}");
            InitAnalytics(indicesString);
        }

        /// <summary>
        /// Initialises the Analytics with a single index
        /// </summary>
        /// <param name="index"></param>
        public void InitAnalytics(string index)
        {
            Indices += index;
        }

        /// <summary>
        /// Default Method for Geting Analytics for searches
        /// </summary>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public async Task<JObject> GetAnalyticsForSearch(AnalyticSearchType searchType)
        {
            return await GetAnalyticsForSearch(new AnalyticsOptions(), searchType);
        }

        /// <summary>
        /// Get Analytics features for Search
        /// Consult Algolia Analytics documentation for supported options
        /// </summary>
        /// <param name="options"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public async Task<JObject> GetAnalyticsForSearch(AnalyticsOptions options, AnalyticSearchType searchType)
        {
            var requestUrl = $"{AnalyticsApiUrl}/searches/{Indices}/{GetSearchResource(searchType)}";

            if (!string.IsNullOrWhiteSpace(options.RequestString))
                requestUrl += options.RequestString;

            var httpRequest = await _httpClient.GetAsync(requestUrl);
            var httpResponse = string.Empty;
            if (httpRequest.IsSuccessStatusCode)
            {
                 httpResponse = await httpRequest.Content.ReadAsStringAsync();
            }
            else
            {
                switch (httpRequest.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                        throw new AlgoliaException("Invalid Incredentials.");
                    case HttpStatusCode.InternalServerError:
                        throw new AlgoliaException("An Internal server error occurred.");
                }
            }


            var result = JObject.Parse(httpResponse);
            return result;
        }

        /// <summary>
        /// Default method for getting analytics for ratelimits
        /// </summary>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public async Task<JObject> GetAnalyticsForRateLimits(AnalyticRateLimitType searchType)
        {
            return await GetAnalyticsForRateLimits(new AnalyticsOptions(), searchType);
        }

        /// <summary>
        /// Get Analytics features for Rate Limits
        /// Consult Algolia Analytics documentation for options
        /// </summary>
        /// <param name="options"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        public async Task<JObject> GetAnalyticsForRateLimits(AnalyticsOptions options, AnalyticRateLimitType searchType)
        {
            var requestUrl = $"{AnalyticsApiUrl}/ratelimits/{Indices}/{GetSearchResource(searchType)}";

            if (!string.IsNullOrWhiteSpace(options.RequestString))
                requestUrl += options.RequestString;

            var httpRequest = await _httpClient.GetAsync(requestUrl);
            var httpResponse = string.Empty;

            if (httpRequest.IsSuccessStatusCode)
            {
                httpResponse = await httpRequest.Content.ReadAsStringAsync();
            }
            else
            {
                switch (httpRequest.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                        throw new AlgoliaException("Invalid Incredentials.");
                    case HttpStatusCode.InternalServerError:
                        throw new AlgoliaException("An Internal server error occurred.");
                }
            }


            var result = JObject.Parse(httpResponse);
            return result;
        }
    }
}