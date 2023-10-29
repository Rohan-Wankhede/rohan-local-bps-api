using DebugApi.Features.Employees;
using DebugApi.Features.Users;

namespace DebugApi.Features;

internal static class EndpointsExtension
{
    public static WebApplication MapControllerEndpoints(this WebApplication app)
    {
        // Employee endpoints
        CreateEmployee.MapEndpoint(app);
        GetEmployee.MapEndpoint(app);
        ListEmployees.MapEndpoint(app);

        // Secret endpoints
        GetSecrets.MapEndpoint(app);

        // Azure b2c User endpoints
        CreateUser.MapEndpoint(app);
        GetUser.MapEndpoint(app);
        ListUsers.MapEndpoint(app);
        EditUser.MapEndpoint(app);

        return app;
    }
}
