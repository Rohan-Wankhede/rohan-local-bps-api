using MediatR;
using DebugApi.Common;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Employees;

namespace DebugApi.Features.Employees;

internal class CreateEmployee
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        var baseUrl = "api/v1/employees";

        app.MapPost(baseUrl, async (
            Request request,
            ISender sender,
            CancellationToken token) =>
            {
                var response = await sender.Send(request, token);

                return Results.Created($"api/v1/employees/{response.Id}", new ApiResponse<Response>()
                {
                    Success = true,
                    Data = response
                });

            })
            .WithDescription("Creates a employee and return its id if succeed.")
            .WithSummary("Create a employee")
            .Produces<ApiResponse<Response>>(StatusCodes.Status201Created)
            .WithOpenApi();

        return app;
    }

    public record Response(
        Guid Id,
        EmployeeGrade Grade,
        string Title,
        string Name);

    public record Request(
        EmployeeGrade Grade,
        string Title,
        string Name

     ) : IRequest<Response>;

    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly AppDbContext _dbContext;

        public RequestHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var employee = Employee.Create(
                request.Grade,
                request.Title,
                request.Name);

            await _dbContext.Employees.AddAsync(employee, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(employee.Id, employee.Grade, employee.Title, employee.Name);

        }
    }
}
