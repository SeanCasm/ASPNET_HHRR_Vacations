using ASPNET_HHRR_Vacations.Helpers;
using ASPNET_HHRR_Vacations.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_HHRR_Vacations.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly EnterpriseContext _enterpriseContext;
        private readonly IPasswordHashService _passwordHashService;

        public AuthService(EnterpriseContext enterpriseContext, IPasswordHashService passwordHashService)
        {
            _enterpriseContext = enterpriseContext;
            _passwordHashService = passwordHashService;
        }
        public Task<bool> ChangePasswordAsync(PasswordChangeDto passwordChange)
        {
            throw new NotImplementedException();
        }


        public async Task<AuthResult> VerifyCredentials(UserCredential loginCredentials)
        {
            var authResult = new AuthResult();

            string fullEmail = loginCredentials.Email.FormatEmail();
            string plainTextPassword = loginCredentials.PasswordHash;

            UserCredential user = await FindUserByEmail(fullEmail);

            if (user == null)
            {
                authResult.IsSuccess = true;
                authResult.ErrorMessage = "User not found";
                return authResult;
            }

            string hashedPassword = _passwordHashService.HashPassword(user, plainTextPassword);
            bool passwordCorrect = _passwordHashService.VerifyHash(user, hashedPassword, plainTextPassword);
            if (passwordCorrect)
            {
                authResult.IsSuccess = true;
                authResult.ErrorMessage = null;
                authResult.ObjectResult = user;
                return authResult;
            }

            authResult.IsSuccess = false;
            authResult.ErrorMessage = "Incorrect password, try again";
            return authResult;
        }
        private async Task<UserCredential> FindUserByEmail(string fullEmail)
        {
            UserCredential? user = await _enterpriseContext.UserCredentials
                    .FirstOrDefaultAsync(u => u.Email == fullEmail);

            return user;
        }

    }
}
