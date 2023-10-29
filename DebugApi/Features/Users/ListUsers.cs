using DebugApi.Common;
using DebugApi.Common.Exceptions;
using DebugApi.Infrastructure.Persistence;
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
            if (response != null && response.Count > 0)
            {
                // Handle the list of responses as needed.
                return Results.Ok(new ApiResponse<IList<Response>>
                {
                    Success = true,
                    Data = response
                });
            }
            else
            {
                // Handle the case where the response list is empty.
                return Results.BadRequest(new ApiResponse<Response>
                {
                    Success = false,
                    Error = new ApiError
                    {
                        Code = "E5678",
                        Message = "No responses were found."
                    },
                    Data = null
                });
            }

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
    public record Request() : IRequest<IList<Response>>
    {
    }
#pragma warning restore S2094 // Classes should not be empty

    public class RequestHandler : IRequestHandler<Request, IList<Response>>
    {
        private readonly IMapper _mapper;

        public RequestHandler(AppDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IList<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            string jsonFilePath = "Common/Data/UserData.json";
            var userlist = await JsonFileReader.ReadJsonFileAsync<AzUser>(jsonFilePath, cancellationToken);

            return _mapper.Map<List<Response>>(userlist ?? throw new EntityNotFoundException(nameof(User))); ;
        }
    }
    
}