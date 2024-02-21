using Microsoft.AspNetCore.Mvc;

namespace ASPNET_HHRR_Vacations.Models;
[ModelMetadataType(typeof(VacationMetadata))]
public partial class Vacation
{
    public int VacationId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<VacationTicket> VacationTickets { get; set; } = new List<VacationTicket>();
}
