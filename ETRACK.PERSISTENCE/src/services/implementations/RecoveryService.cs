using Microsoft.EntityFrameworkCore;
using src.models;
using src.services.interfaces;

namespace src.services.concrete;

public sealed class RecoveryService : IRecoveryService
{
    private readonly ICryptographyService _cryptographyService;
    private readonly RailwayContext _context;
    public RecoveryService(ICryptographyService cryptographyService)
    {
        _context = new RailwayContext();
        _cryptographyService = cryptographyService;
    }
    public async Task<bool> ChangePassword(string secret)
    {
        throw new NotImplementedException();
    }
} 