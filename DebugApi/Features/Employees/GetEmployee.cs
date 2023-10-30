using DebugApi.Common;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Common.ValueObjects;
using DebugDomain.Employees;
using MapsterMapper;
using MediatR;

namespace DebugApi.Features.Employees;

internal class GetEmployee
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/employees/{id}", async (Guid id, ISender sender, CancellationToken token) =>
        {
            var response = await sender.Send(new Request(id), token);
            return response.Success ? Results.Ok(response) : Results.NotFound(response);
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

    public record Request(Guid Id) : IRequest<ApiResponse<Response>>;

    public class RequestHandler : IRequestHandler<Request, ApiResponse<Response>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public RequestHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees
                   .FindAsync(new object[] { (Id)request.Id }, cancellationToken)!;

            if (employee == null)
            {
                return ApiResponseHelper.ErrorResponse<Response>("EntityNotFound", $"Employee with ID {request.Id} was not found.");
            }
            return ApiResponseHelper.SuccessResponse(_mapper.Map<Response>(employee));
        }
    }
}