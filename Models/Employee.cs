using Microsoft.AspNetCore.Mvc;

namespace ASPNET_HHRR_Vacations.Models;
[ModelMetadataType(typeof(EmployeeMetadata))]
public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string CompleteName => $"{FirstName} {LastName}";

    public virtual UserCredential? UserCredential { get; set; }

    public virtual ICollection<VacationTicket> VacationTickets { get; set; } = new List<VacationTicket>();

    public virtual ICollection<Vacation> Vacations { get; set; } = new List<Vacation>();
}
