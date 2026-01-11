using API.Models;

namespace API.Factories;

public static class APIResponseFactory
{  
    public static APIResponse<T> CreateSuccess<T>(T? data)
    {
        return new APIResponse<T>
        {
            Success = true,
            Data = data,
        };
    }

    public static APIResponse<T> CreateError<T>(string error)
    {
        return new APIResponse<T>
        {
            Success = false,
            Error = error
        };
    }

}
