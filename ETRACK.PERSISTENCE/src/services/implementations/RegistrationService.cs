using System.Security.Cryptography;
using System.Text;
using src.dto.request;
using src.dto.response;
using src.models;
using src.services.interfaces;
namespace src.services.concrete;
public class RegistrationService : IRegistrationService
{
    private readonly RailwayContext _context;
    private readonly IConfigurationRoot configurationRoot;
    private readonly ICryptographyService _cryptographyService;
    public RegistrationService(RailwayContext context, ICryptographyService cryptographyService)
    {
        _context = context;
        _cryptographyService = cryptographyService;
        configurationRoot = new ConfigurationBuilder().AddUserSecrets<RegistrationService>().Build();        
    }
    
    Task<int> IRegistrationService.CheckRegistrationAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    Task<RegisterResponse> IRegistrationService.RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }
}