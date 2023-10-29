using MapsterMapper;
using MediatR;
using DebugApi.Common;
using DebugApi.Common.Exceptions;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Common.ValueObjects;
using DebugDomain.Users;
using System.Text.Json;
using System.Linq;

namespace DebugApi.Features.Users;

internal class GetUser
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/users/{id}", async (Guid id, ISender sender, CancellationToken token) =>
        {
            var response = await sender.Send(new Request(id), token);
            return Results.Ok(new ApiResponse<Response>
            {
                Success = true,
                Data = response
            });
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

    public record Request(Guid Id) : IRequest<Response>;

    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public RequestHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            string jsonFilePath = "Common/Data/UserData.json";
            var userlist = await JsonFileReader.ReadJsonFileAsync<AzUser>(jsonFilePath, cancellationToken);

            AzUser user = userlist.FirstOrDefault(userlist => userlist.Id == request.Id)!;

            return _mapper.Map<Response>(user ?? throw new EntityNotFoundException(nameof(User), request.Id));

        }
    }
}
