using src.models;
using src.dto.request;
using src.dto.response;
using src.services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace src.services.concrete;
public class AuthService: IAuthService
{
    private readonly RailwayContext _context;
    private readonly ICryptographyService _cryptographyService;
    private readonly IJWTService _jwtService;
    public AuthService(RailwayContext context, ICryptographyService cryptographyService, IJWTService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
        _cryptographyService = cryptographyService;      
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        User _user = await _context.Users.FirstOrDefaultAsync(usr => usr.Login.Username == request.username);
        if (_user is null)
            return new LoginResponse(401, "Unauthorized");

        string ChallengeHash = _cryptographyService.HashString(_user?.Login?.Salt, request.password);

        if (await this._cryptographyService.CompareHash(ChallengeHash, _user.Login.PasswordHash))
            return new LoginResponse(401, "Unauthorized");

        string jwtToken = await _jwtService.CreateJWT(new JWTCreateRequest(_user.UserId.ToString(), _user.Login.LoginId, _user.Login.Username));
        return new LoginResponse(201, jwtToken);
    }
}