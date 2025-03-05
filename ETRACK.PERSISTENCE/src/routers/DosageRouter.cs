using Microsoft.AspNetCore.Mvc;
using src.dto.request.dosages;
using src.services.interfaces;
namespace src.routers;
public static class DosageRouter
{
    public static void MapDosageRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/Dosage/create", async (IJWTService _jwtService, IDosageService _dosageService, [FromHeader] string jwt, [FromBody] CreateDosageRequest request ) => 
        {
            try
            {
                string _LoginID = await _jwtService.ValidateJWT(jwt);
                if(!(_LoginID == request.UserID))
                {
                    return new CreateDosageResponse(401, "NOT AUTHORIZED", null);
                }
                return await _dosageService.CreateDosage(request);

            }
            catch (System.Exception ex)
            {
                return new CreateDosageResponse(400,ex.Message,null);
                
            }
        });

    }

}