using src.dto.request;
using src.dto.response;
using Microsoft.AspNetCore.Mvc;

namespace src.services.interfaces;
interface IRegistrationService {
    public Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    public Task<int> CheckRegistrationAsync(RegisterRequest request);
}