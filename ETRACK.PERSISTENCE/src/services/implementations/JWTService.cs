using src.models;
using src.services.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public sealed class JWTService : IJWTService
{
    private readonly IConfigurationRoot _configurationRoot;

    public JWTService()
    {
        _configurationRoot = new ConfigurationBuilder().AddUserSecrets<JWTService>().Build();
    }
    public Task<string> CreateJWT(JWTCreateRequest request)
    {
        IEnumerable<Claim> claims = this.GenerateClaimsList(request);
        SecurityTokenDescriptor securityTokenDescriptor = this.GenerateSecurityTokenDescriptor(claims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        string jwt_string = jwtSecurityTokenHandler.WriteToken(token);
        return Task.FromResult(jwt_string);
    }
    //TODO : make this return the LoginID of the user so we can do funnies.
    public Task<bool> ValidateJWT(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
            return Task.FromResult(false);
        TokenValidationParameters tokenValidationParameters = ConfigureTokenValidationParameters();

        JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        try 
        {
            ClaimsPrincipal _ValidJWTToken = _jwtSecurityTokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out SecurityToken token);
            
            return Task.FromResult(true);
        }
        catch (System.Exception ex)
        {
            return Task.FromResult(false);
        }
    }
    private IEnumerable<Claim> GenerateClaimsList(JWTCreateRequest request)
    {
        IEnumerable<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, request.Login_id),
            new Claim(JwtRegisteredClaimNames.Sub, request.User_id),
            new Claim(JwtRegisteredClaimNames.Name, request.username),
            new Claim(JwtRegisteredClaimNames.UniqueName, request.username)
        };
        return claims;
    }
    private SecurityTokenDescriptor GenerateSecurityTokenDescriptor(IEnumerable<Claim> claimsList)
    {

        SecurityTokenDescriptor securityTokenDescriptor= new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claimsList),
            Expires = DateTime.Now.AddDays(14),
            Issuer = "127.0.0.1",
            Audience = "127.0.0.1",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configurationRoot["JWT_SECRET"])), SecurityAlgorithms.HmacSha256),
            IssuedAt = DateTime.Now
        };
        return securityTokenDescriptor;
    }
    private TokenValidationParameters ConfigureTokenValidationParameters()
    {
        TokenValidationParameters _validationParameters = new TokenValidationParameters
        {
            ValidAudience = "127.0.0.1",
            ValidIssuer = "127.0.0.1",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["JWT_SECRET"])), 
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
        return _validationParameters;
    }
}