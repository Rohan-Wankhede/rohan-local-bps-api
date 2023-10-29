namespace DebugApi.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public ApiError? Error { get; set; }
    public T? Data { get; set; }
}

public class ApiError
{
    public string? Code { get; set; }
    public string? Message { get; set; }
}

