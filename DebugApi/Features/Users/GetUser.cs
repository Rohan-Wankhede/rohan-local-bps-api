using DebugApi.Common;
using DebugApi.Common.Exceptions;
using DebugDomain.Employees;
using DebugDomain.Users;
using MapsterMapper;
using MediatR;

namespace DebugApi.Features.Users;

internal class GetUser
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/users/{id}", async (Guid id, ISender sender, CancellationToken token) =>
        {
            var response = await sender.Send(new Request(id), token);
            return response.Success ? Results.Ok(response) : Results.NotFound(response);
        })
        .WithDescription("Get users by its id.")
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

    public record Request(Guid Id) : IRequest<ApiResponse<Response>>;

    public class RequestHandler : IRequestHandler<Request, ApiResponse<Response>>
    {
        private readonly IMapper _mapper;

        public RequestHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ApiResponse<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            string jsonFilePath = "Common/Data/UserData.json";
            var userlist = await JsonFileReader.ReadJsonFileAsync<AzUser>(jsonFilePath, cancellationToken);

            AzUser user = userlist.FirstOrDefault(userlist => userlist.Id == request.Id)!;
            if (user == null)
            {
                return ApiResponseHelper.ErrorResponse<Response>("EntityNotFound", $"Employee with ID {request.Id} was not found.");
            }
            return ApiResponseHelper.SuccessResponse(_mapper.Map<Response>(user));

        }
    }
}
