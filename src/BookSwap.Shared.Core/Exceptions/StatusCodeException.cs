using System.Net;

namespace BookSwap.Shared.Core.Exceptions
{
    public abstract class StatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        protected StatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
