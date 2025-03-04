using System.Security.Cryptography;
using System.Text;
using src.services.interfaces;
namespace src.services.concrete;
public sealed class CryptographyService : ICryptographyService
{
    private readonly IConfigurationRoot _configurationRoot;
    public CryptographyService()
    {
        _configurationRoot = new ConfigurationBuilder().AddUserSecrets<RegistrationService>().Build();        
    }
    public string HashString(string _salt, string _str)
    {
        HMACSHA256 _hashingAlg = new HMACSHA256(Encoding.UTF8.GetBytes(_configurationRoot["Secret"]));
        byte[] _computedHash = _hashingAlg.ComputeHash(Encoding.UTF8.GetBytes($"{_str}{_salt}"));
        return Encoding.UTF8.GetString(_computedHash);
    }

    public string SaltGen()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 20)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public Task<bool> CompareHash(string _challengeHash, string _passwordHash)
    {
        if (_challengeHash == _passwordHash)
            return Task.FromResult(true);
        else 
            return Task.FromResult(false);
    }
}