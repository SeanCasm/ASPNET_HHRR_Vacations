using ASPNET_HHRR_Vacations.Models;

namespace ASPNET_HHRR_Vacations.Services.Authentication
{
    public interface IPasswordHashService
    {
        string HashPassword(UserCredential credentials, string plainTextPassword);
        bool VerifyHash(UserCredential credentials, string password, string hashedPassword);
    }
}
