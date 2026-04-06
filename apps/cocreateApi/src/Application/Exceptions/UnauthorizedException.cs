using System.Net;

namespace Application.Exceptions;

public class UnauthorizedException(string message, Exception? innerException = null)
    : AppException(message, innerException)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string ErrorCode => "error.unauthorized";
}
