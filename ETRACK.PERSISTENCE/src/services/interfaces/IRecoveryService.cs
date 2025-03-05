namespace src.services.interfaces;
public interface IRecoveryService
{
    public Task<bool> ChangePassword(string secret);
}