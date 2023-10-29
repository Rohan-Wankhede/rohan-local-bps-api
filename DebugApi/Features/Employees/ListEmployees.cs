using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DebugApi.Common;
using DebugApi.Common.Exceptions;
using DebugApi.Infrastructure.Persistence;
using DebugDomain.Employees;

namespace DebugApi.Features.Employees;

internal class ListEmployees
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/employees", async (ISender sender, CancellationToken token) =>
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
    public record Request() : IRequest<IList<Response>>
    {
    }
#pragma warning restore S2094 // Classes should not be empty

    public class RequestHandler : IRequestHandler<Request, IList<Response>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public RequestHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IList<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees
                .ToListAsync(cancellationToken);

            var abc = _mapper.Map<List<Response>>(employee ?? throw new EntityNotFoundException(nameof(Employee)));
            return abc;
        }
    }
}
