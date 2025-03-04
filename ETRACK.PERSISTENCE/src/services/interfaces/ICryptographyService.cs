namespace src.services.interfaces;

public interface ICryptographyService
{
    public string HashString(string _salt, string _str);

    public string SaltGen();
    public Task<bool> CompareHash(string _challengeHash, string _passwordHash);

}