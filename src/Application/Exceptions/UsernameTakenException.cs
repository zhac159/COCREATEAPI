using System.Net;

namespace Application.Exceptions;

public class UsernameTakenException(string message, Exception? innerException = null)
    : AppException(message, innerException)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string ErrorCode => "error.username-taken";
}
