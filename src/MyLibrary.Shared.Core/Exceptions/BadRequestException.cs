using System.Net;

namespace MyLibrary.Shared.Core.Exceptions;

public class BadRequestException : StatusCodeException
{
    public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message) { }
}

