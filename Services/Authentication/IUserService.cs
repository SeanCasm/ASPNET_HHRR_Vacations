using ASPNET_HHRR_Vacations.Models;

namespace ASPNET_HHRR_Vacations.Services.Authentication
{
    public interface IUserService
    {
        Task<AuthResult> CreateUser(Employee employee);
    }
}
