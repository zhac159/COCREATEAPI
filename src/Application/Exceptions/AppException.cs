using System.Net;

namespace Application.Exceptions;

public class AppException(string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public virtual HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    public virtual string ErrorCode => "error";
}
