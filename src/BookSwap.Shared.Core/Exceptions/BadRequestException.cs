using System.Net;

namespace BookSwap.Shared.Core.Exceptions;

public class BadRequestException : StatusCodeException
{
    public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }
}

