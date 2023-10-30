namespace DebugApi.Common;

public class ApiResponseHelper
{
    public static ApiResponse<T> SuccessResponse<T>(T data)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
        };
    }

    public static ApiResponse<T> ErrorResponse<T>(string errorCode, string errorMessage)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Error = new ApiError
            {
                Code = errorCode,
                Message = errorMessage
            }
        };
    }
}
