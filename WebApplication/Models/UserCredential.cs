using Microsoft.AspNetCore.Mvc;

namespace ASPNET_HHRR_Vacations.Models;
[ModelMetadataType(typeof(UserCredentialMetadata))]
public partial class UserCredential
{
    public int UserId { get; set; }

    public int EmployeeId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
