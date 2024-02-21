using ASPNET_HHRR_Vacations.Helpers;
using ASPNET_HHRR_Vacations.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_HHRR_Vacations.Services.VacationRequests
{
    public class VacationRepository : Repository, IVacationRepository
    {
        public VacationRepository(EnterpriseContext enterpriseContext) : base(enterpriseContext)
        {
        }

        public async Task Approve(VacationTicket ticket)
        {
            ticket.RequestId = TicketRequestStatus.approved;
            await Save();
        }

        public async Task CreateVacationTicket(Vacation vacationData)
        {
            VacationTicket vacationTicket = new()
            {
                RequestId = TicketRequestStatus.sent,
                EmployeeId = vacationData.EmployeeId,
            };

            await _enterpriseContext.Vacations.AddAsync(vacationData);
            await Save();

            vacationTicket.VacationId = vacationData.VacationId;
            await _enterpriseContext.VacationTickets.AddAsync(vacationTicket);
            await Save();
        }

        public async Task Decline(VacationTicket entity)
        {
            entity.RequestId = TicketRequestStatus.declined;
            await Save();
        }

        public Task Delete(VacationTicket vacation)
        {
            throw new NotImplementedException();
        }

        public async Task<VacationTicket> FindById(int id)
        {
            VacationTicket? ticket = await _enterpriseContext.VacationTickets.FindAsync(id);
            return ticket;
        }

        public async Task<IEnumerable<VacationTicket>> GetVacationsTickets(int employeeId)
        {
            IEnumerable<VacationTicket> vacations = await _enterpriseContext.VacationTickets
             .Where(v => v.EmployeeId == employeeId)
             .Include(v => v.Request)
             .Include(v => v.Vacation)
             .ToListAsync();

            return vacations;
        }
    }
}
