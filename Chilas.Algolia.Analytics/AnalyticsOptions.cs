using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilas.Algolia.Analytics
{
    public class AnalyticsOptions
    {
        private int _size;
        private DateTime _startAt;
        private DateTime _endAt;
        private IEnumerable<string> _tags;
        private string _country;
        private bool _refinements;

        public AnalyticsOptions SetSize(int size)
        {
            _size = size;
            return this;
        }

        public AnalyticsOptions StartAt(DateTime date)
        {
            _startAt = date;
            return this;
        }

        public AnalyticsOptions EndAt(DateTime date)
        {
            _endAt = date;
            return this;
        }

        public AnalyticsOptions SetTags(IEnumerable<string> tags)
        {
            _tags = tags;
            return this;
        }

        public AnalyticsOptions SetCountry(string country)
        {
            _country = country;
            return this;
        }

        public AnalyticsOptions SetRefinements(bool refinements)
        {
            _refinements = refinements;
            return this;
        }

        internal string RequestString {
            get {

                var queryString = "?";

                if (_refinements) queryString += $"refinements={_refinements}&";
                if (!string.IsNullOrWhiteSpace(_country)) queryString += $"country={_country}&";
                if (_size >= 0) queryString += $"size={_size}";
                if (_tags.ToArray().Length > 0)
                {
                    var tags = _tags.Aggregate(string.Empty, (current, tag) => current + $"{tag},");
                    tags = tags.Remove(tags.Length - 1, 1);
                    queryString += $"tags={tags},";
                }
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
                if (_startAt <= DateTime.Today)
                {
                    var timeSpan = _startAt.ToLocalTime() - epoch;
                    queryString += $"startAt={timeSpan},";
                }

                if (_endAt <= DateTime.Today)
                {
                    var timeSpan = _endAt.ToLocalTime() - epoch;
                    queryString += $"endAt={timeSpan},";
                }

                queryString = queryString.Remove(queryString.Length - 1, 1);

                return queryString;
            }
        }
    }
}