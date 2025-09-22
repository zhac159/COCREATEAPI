namespace API.Factories;

public class APIResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Error { get; set; }
    public string? ErrorCode { get; set; }
}

public static class APIResponseFactory
{
    public static APIResponse<T> CreateSuccess<T>(T? data)
    {
        return new APIResponse<T> { Success = true, Data = data };
    }

    public static APIResponse<T> CreateError<T>(string errorCode, string error)
    {
        return new APIResponse<T>
        {
            Success = false,
            Error = error,
            ErrorCode = errorCode,
        };
    }
}
