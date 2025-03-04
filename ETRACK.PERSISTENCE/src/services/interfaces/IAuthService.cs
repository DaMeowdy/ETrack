using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
namespace src.services.interfaces;
public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest request);
}