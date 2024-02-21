using ASPNET_HHRR_Vacations.Models;
namespace ASPNET_HHRR_Vacations.Services.Employees
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> FindByIdAndIncludeCredentials(int id);
    }
}
