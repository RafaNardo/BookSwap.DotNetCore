using System.Net;

namespace BookSwap.Shared.Core.Exceptions
{
    public class NotFoundException : StatusCodeException
    {
        public NotFoundException() : base(HttpStatusCode.NotFound, "The requested resource was not found") { }
    }
}
