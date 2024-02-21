using ASPNET_HHRR_Vacations.Models;

namespace ASPNET_HHRR_Vacations.Services.Authentication
{
    public interface IAuthService
    {
        Task<bool> ChangePasswordAsync(PasswordChangeDto passwordChange);
        Task<AuthResult> VerifyCredentials(UserCredential credentials);
    }
}
