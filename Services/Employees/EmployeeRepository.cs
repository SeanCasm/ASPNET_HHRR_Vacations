using ASPNET_HHRR_Vacations.Models;
using Microsoft.EntityFrameworkCore;
namespace ASPNET_HHRR_Vacations.Services.Employees
{
    public class EmployeeRepository : Repository, IEmployeeRepository
    {
        public EmployeeRepository(EnterpriseContext enterpriseContext) : base(enterpriseContext)
        {
        }
        public async Task Delete(Employee entity)
        {
            await _enterpriseContext
                    .Entry(entity)
                    .Collection(e => e.VacationTickets)
                    .LoadAsync();

            await _enterpriseContext.Entry(entity)
                .Collection(e => e.Vacations)
                .LoadAsync();

            entity.VacationTickets.Clear();
            entity.Vacations.Clear();
            _enterpriseContext.UserCredentials.Remove(entity.UserCredential);
            _enterpriseContext.Employees.Remove(entity);

            await Save();
        }

        public async Task<Employee> FindById(int employeeId)
        {
            var employeeDb = await _enterpriseContext
                            .Employees
                            .FindAsync(employeeId);
            return employeeDb;
        }

        public async Task<Employee> FindByIdAndIncludeCredentials(int id)
        {
            var employeeDb = await _enterpriseContext
                            .Employees
                            .Include(e => e.UserCredential)
                            .Where(e => e.EmployeeId == id)
                            .FirstOrDefaultAsync();

            return employeeDb;
        }
    }
}
