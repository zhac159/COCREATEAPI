using System.Net;

namespace Application.Exceptions;

public class UsernameOrEmailNotFoundException(string message, Exception? innerException = null)
    : AppException(message, innerException)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string ErrorCode => "error.username-or-email-not-found";
}
