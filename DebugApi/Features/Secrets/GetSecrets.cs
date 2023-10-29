using MediatR;
using Microsoft.Extensions.Options;
using DebugApi.Common;

namespace DebugApi.Features.Employees;

public class GetSecrets
{
    public static WebApplication MapEndpoint(WebApplication app)
    {
        app.MapGet("api/v1/secrets", async (ISender sender, CancellationToken token) =>
        {
            var response = await sender.Send(new Request(), token);

            return Results.Ok(response);

        })
        .WithDescription("Get Secret value from Key Vault.")
        .WithSummary("Get Secret")
        .Produces<Response>()
        .WithOpenApi();

        return app;
    }

    public record Response(string Value);

#pragma warning disable S2094 // Classes should not be empty
    public record Request() : IRequest<Response>;
#pragma warning restore S2094 // Classes should not be empty

    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly KeyVaultOptions _options;

        public RequestHandler(IOptions<KeyVaultOptions> options)
        {
            _options = options.Value;
        }

        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Response(_options.KeyVaultSecret!));
        }
    }
}
