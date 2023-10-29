using MapsterMapper;
using MediatR;
using DebugApi.Common;
using DebugApi.Common.Exceptions;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Common.ValueObjects;
using DebugDomain.Employees;

namespace DebugApi.Features.Employees;

internal class GetEmployee
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/employees/{id}", async (Guid id, ISender sender, CancellationToken token) =>
        {
            var response = await sender.Send(new Request(id), token);
            return Results.Ok(new ApiResponse<Response>
            {
                Success = true,
                Data = response
            });
        })
        .WithDescription("Get employee by its id.")
        .WithSummary("Get employee")
        .Produces<ApiResponse<Response>>()
        .WithOpenApi(); 

        return app;
    }

    public record Response(
        Guid Id,
        EmployeeGrade Grade,
        string Title,
        string Name
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
            var employee = await _dbContext.Employees
                .FindAsync(new object[] { (Id)request.Id }, cancellationToken);

            return _mapper.Map<Response>(employee ?? throw new EntityNotFoundException(nameof(Employee), request.Id));
        }
    }
}
