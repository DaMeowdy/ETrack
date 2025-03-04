
namespace src.services.interfaces;
public interface IJWTService 
{
    Task<string> CreateJWT(JWTCreateRequest request);
    Task<bool> ValidateJWT(string jwtToken);
}