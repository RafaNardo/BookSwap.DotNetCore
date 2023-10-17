using System.Net;

namespace MyLibrary.Shared.Core.Exceptions
{
    public class NotFoundException : StatusCodeException
    {
        public NotFoundException() : base(HttpStatusCode.NotFound, "The requested resource was not found") { }
    }
}
