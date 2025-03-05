
namespace src.services.interfaces;
public interface IJWTService 
{
    Task<string> CreateJWT(JWTCreateRequest request);
    Task<string> ValidateJWT(string jwtToken);
}