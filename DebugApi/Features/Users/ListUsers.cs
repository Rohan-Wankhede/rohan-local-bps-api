using DebugApi.Common;
using DebugDomain.Users;
using MapsterMapper;
using MediatR;

namespace DebugApi.Features.Users;

internal class ListUsers
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/users", async (ISender sender, CancellationToken token) =>
        {
            var response = await sender.Send(new Request(), token);
            return response.Success ? Results.Ok(response) : Results.NotFound(response);

        })
        .WithDescription("Get all of the user list.")
        .WithSummary("Get users")
        .Produces<ApiResponse<Response>>()
        .WithOpenApi();

        return app;
    }

    public record Response(
        Guid Id,
        string UserName,
        string SurName,
        string UserEmail,
        string UserRole,
        UserStatus UserStatus
    );

#pragma warning disable S2094 // Classes should not be empty
    public record Request() : IRequest<ApiResponse<List<Response>>>
    {
    }
#pragma warning restore S2094 // Classes should not be empty

    public class RequestHandler : IRequestHandler<Request, ApiResponse<List<Response>>>
    {
        private readonly IMapper _mapper;

        public RequestHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<Response>>> Handle(Request request, CancellationToken cancellationToken)
        {
            string jsonFilePath = "Common/Data/UserData.json";
            var userlist = await JsonFileReader.ReadJsonFileAsync<AzUser>(jsonFilePath, cancellationToken);

            if (userlist == null || userlist.Count == 0)
            {
                return ApiResponseHelper.ErrorResponse<List<Response>>("EntityNotFound", "No User records found !!.");
            }
            var response = _mapper.Map<List<Response>>(userlist);
            return ApiResponseHelper.SuccessResponse(response);
        }
    }
}