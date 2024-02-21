using ASPNET_HHRR_Vacations.Models;
using Microsoft.AspNetCore.Identity;

namespace ASPNET_HHRR_Vacations.Services.Authentication
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(UserCredential credentials, string plainTextPassword)
        {
            var passwordHasher = new PasswordHasher<UserCredential>();
            var hashedPassword = passwordHasher.HashPassword(credentials, plainTextPassword);
            return hashedPassword;
        }

        public bool VerifyHash(UserCredential credentials, string hashedPassword, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<UserCredential>();
            var result = passwordHasher.VerifyHashedPassword(credentials, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
