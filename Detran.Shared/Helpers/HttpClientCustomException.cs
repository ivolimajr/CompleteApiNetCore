using System;

namespace Detran.Shared.Helpers
{
    public class HttpClientCustomException : Exception
    {
        public int StatusCode { get; set; }
        public HttpClientCustomException()
        {
        }

        public HttpClientCustomException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpClientCustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}