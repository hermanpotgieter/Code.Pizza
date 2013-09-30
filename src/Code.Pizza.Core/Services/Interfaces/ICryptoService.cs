namespace Code.Pizza.Core.Services.Interfaces
{
    public interface ICryptoService
    {
        string GenerateSalt(string password);

        string ComputeHash(string salt, string password);

        bool VerifyPassword(string salt, string hash, string password);
    }
}
