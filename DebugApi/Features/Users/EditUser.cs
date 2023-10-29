using DebugApi.Common;
using DebugApi.Common.Exceptions;
using DebugDomain.Users;
using MapsterMapper;
using MediatR;

namespace DebugApi.Features.Users;

internal class EditUser
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapPut("api/v1/users/{id}", async (Request request, ISender sender, CancellationToken token) =>
        {
            var response = await sender.Send(request, token);
            return Results.Ok(new ApiResponse<Response>
            {
                Success = true,
                Data = response
            });

        })
            .WithDescription("Edit User's information by ID.")
            .WithSummary("Edit User")
            .Produces<ApiResponse<Response>>(StatusCodes.Status200OK)
            .Produces<object>(StatusCodes.Status404NotFound)
            .WithOpenApi();

        return app;
    }
    public record Response(
       Guid Id,
       string UserName,
       string SurName,
       string UserEmail,
       string UserRole,
       UserStatus Status
    );

    public record Request(
       Guid Id,
       string UserName,
       string SurName,
       string UserEmail,
       string UserRole,
       UserStatus UserStatus

     ) : IRequest<Response>;

    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IMapper _mapper;

        public RequestHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            string jsonFilePath = "Common/Data/UserData.json";
            var userlist = await JsonFileReader.ReadJsonFileAsync<AzUser>(jsonFilePath, cancellationToken);

            AzUser user = userlist.FirstOrDefault(user => user.Id == request.Id)!;
            if (user != null)
            {
                user.UserName = request.UserName;
                user.SurName = request.SurName;
                user.UserEmail = request.UserEmail;
                user.UserRole = request.UserRole;
                user.UserStatus = request.UserStatus;
            }
            else
            {
                throw new EntityNotFoundException(nameof(User), request.Id);
            }
            return new Response(user.Id, user.UserName, user.SurName, user.UserEmail, user.UserRole, user.UserStatus);
        }
    }
}
