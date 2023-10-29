using MapsterMapper;
using MediatR;
using DebugApi.Common;
using DebugApi.Common.Exceptions;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Common.ValueObjects;
using DebugDomain.Users;

namespace DebugApi.Features.Users;

internal class EditUser
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        // I am just checking git integration 
        app.MapPut("api/v1/users/{id}", async ( Guid id, Request request, ISender sender, CancellationToken token) =>
        {
            var response = await sender.Send(new Request(id), token);
            return Results.Ok(new ApiResponse<Response>
            {
                Success = true,
                Data = response
            });

        })
            .WithDescription("Updates an employee's information by ID.")
            .WithSummary("Update an employee")
            .Produces<ApiResponse<Response>>(StatusCodes.Status200OK)
            .Produces<object>(StatusCodes.Status404NotFound)
            .WithOpenApi();

        return app;
    }
    public record Response(
        Guid Id,
        UserName Name,
        SurName Surname,
        UserEmail Email,
        UserRole Role,
        UserStatus Status
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

            return _mapper.Map<Response>(user ?? throw new EntityNotFoundException(nameof(User), request.Id));
        }
    }
}
