using System;

namespace Chilas.Algolia.Analytics
{
    public class AlgoliaException : Exception
    {
        public AlgoliaException(string message)
            : base(message)
        {
        }
    }
}
