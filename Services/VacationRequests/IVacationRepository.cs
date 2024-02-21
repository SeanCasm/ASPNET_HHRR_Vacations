using ASPNET_HHRR_Vacations.Models;

namespace ASPNET_HHRR_Vacations.Services.VacationRequests
{
    public interface IVacationRepository: IRepository<VacationTicket>
    {
        Task Approve(VacationTicket ticket);
        Task Decline(VacationTicket entity);
        Task CreateVacationTicket(Vacation vacationData);
        Task<IEnumerable<VacationTicket>> GetVacationsTickets(int employeeId);
        Task Save();
    }
}
