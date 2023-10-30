using DebugApi.Common;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Employees;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebugApi.Features.Employees;

internal class ListEmployees
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/employees", async (ISender sender, CancellationToken token) =>
        {

            var response = await sender.Send(new Request(), token);
            return response.Success ? Results.Ok(response) : Results.NotFound(response);

        })
            .WithDescription("Get all of the employees list.")
            .WithSummary("Get employees")
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

#pragma warning disable S2094 // Classes should not be empty
    public record Request() : IRequest<ApiResponse<List<Response>>>
    {
    }
#pragma warning restore S2094 // Classes should not be empty

    public class RequestHandler : IRequestHandler<Request, ApiResponse<List<Response>>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public RequestHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<Response>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var employees = await _dbContext.Employees
               .ToListAsync(cancellationToken);

            if (employees == null || employees.Count == 0)
            {
                return ApiResponseHelper.ErrorResponse<List<Response>>("EntityNotFound", "No Employee records found !!.");
            }
            var response = _mapper.Map<List<Response>>(employees);
            return ApiResponseHelper.SuccessResponse(response);
        }

    }
}
