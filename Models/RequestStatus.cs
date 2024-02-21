using System;
using System.Collections.Generic;

namespace ASPNET_HHRR_Vacations.Models;

public partial class RequestStatus
{
    public int RequestId { get; set; }

    public string? RequestType { get; set; }

    public virtual ICollection<VacationTicket> VacationTickets { get; set; } = new List<VacationTicket>();
}
