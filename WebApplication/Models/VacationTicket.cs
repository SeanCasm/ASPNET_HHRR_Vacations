using Microsoft.AspNetCore.Mvc;

namespace ASPNET_HHRR_Vacations.Models;
[ModelMetadataType(typeof(VacationTicketMetadata))]
public partial class VacationTicket
{
    public int TicketId { get; set; }

    public int VacationId { get; set; }

    public int RequestId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime Issued { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual RequestStatus Request { get; set; } = null!;

    public virtual Vacation Vacation { get; set; } = null!;
}
