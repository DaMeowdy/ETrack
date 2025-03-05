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
    public AuthService(ICryptographyService cryptographyService, IJWTService jwtService)
    {
        _context = new RailwayContext();
        _jwtService = jwtService;
        _cryptographyService = cryptographyService;      
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        Login _user = await _context.Logins.SingleOrDefaultAsync<Login?>(login => login.Username == request.username);
        if (_user is null)
            return new LoginResponse(401, "Unauthorized");

        string ChallengeHash = _cryptographyService.HashString(_user.Salt, request.password);

        if (!await this._cryptographyService.CompareHash(ChallengeHash, _user.PasswordHash))
            return new LoginResponse(401, "Unauthorized");

        User AssociatedUser = await this._context.Users.SingleOrDefaultAsync<User?>(_usr => _usr.UserId == _user.UserId);
        if (AssociatedUser is null)
            return new LoginResponse(401, "Unauthorized");
        string jwtToken = await _jwtService.CreateJWT(new JWTCreateRequest(AssociatedUser.UserId.ToString(), _user.LoginId, _user.Username));
        return new LoginResponse(201, jwtToken);
    }
}