namespace DebugApi.Common.Exceptions;

public class ExceptionHandler
{
    public static ApiResponse<T> HandleException<T>(Exception ex)
    {
        if (ex is EntityNotFoundException entityNotFoundException)
        {
            return ApiResponseHelper.ErrorResponse<T>(entityNotFoundException.Code, entityNotFoundException.Message);
        }
        else
        {
            return ApiResponseHelper.ErrorResponse<T>("InternalServerError", "An unexpected error occurred while processing your request.");
        }
    }
}

