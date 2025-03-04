using Microsoft.AspNetCore.Mvc;
using src.services.interfaces;
namespace src.routers;
public static class AuthenticationRouter
{
    public static void MapAuthenticationRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/auth/login", async (IAuthService authService, [FromBody] LoginRequest request ) => 
        {
            LoginResponse response = await authService.Login(request);
            return response;
        });
    }

}