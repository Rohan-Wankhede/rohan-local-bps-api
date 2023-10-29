using DebugApi.Common;
using DebugDomain.Users;
using MediatR;

namespace DebugApi.Features.Users;
// Rohan - To be deleted later when integrated with Azure B2c
internal class CreateUser
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        var baseUrl = "api/v1/user";

        app.MapPost(baseUrl, async (
            Request request,
            ISender sender,
            CancellationToken token) =>
        {
            var response = await sender.Send(request, token);

            return Results.Created($"api/v1/user/{response.Id}", new ApiResponse<Response>()
            {
                Success = true,
                Data = response
            });

        })
            .WithDescription("Creates a user and return its id if succeed.")
            .WithSummary("Create a user")
            .Produces<ApiResponse<Response>>(StatusCodes.Status201Created)
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
    public record Request(
       string UserName,
       string SurName,
       string UserEmail,
       string UserRole,
       UserStatus UserStatus

     ) : IRequest<Response>;

    public class RequestHandler : IRequestHandler<Request, Response>
    {
        public RequestHandler() { }
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            string jsonFilePath = "Common/Data/UserData.json";
            var userlist = await JsonFileReader.ReadJsonFileAsync<AzUser>(jsonFilePath, cancellationToken);

            AzUser user = new AzUser
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                SurName = request.SurName,
                UserEmail = request.UserEmail,
                UserRole = request.UserRole,
                UserStatus = request.UserStatus
            };
            userlist.Add(user);
            return new Response(user.Id, user.UserName, user.SurName, user.UserEmail, user.UserRole, user.UserStatus);

        }
    }
}
