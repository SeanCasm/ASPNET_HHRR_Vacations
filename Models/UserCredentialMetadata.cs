using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASPNET_HHRR_Vacations.Models
{
    public class UserCredentialMetadata
    {
        [Key]
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "The email is required")]
        [RegularExpression("^[a-z]+(\\.[a-z]+)?$", ErrorMessage = "Only lower letters and '.' symbol between are allowed")]
        public string Email { get; set; } = null!;
        [MinLength(8, ErrorMessage = "The password must have at less 8 characters")]
        [MaxLength(20, ErrorMessage = "The password mustn't have more than 20 characters")]
        [Required(ErrorMessage = "The password is required")]
        [DisplayName("Password")]
        public string PasswordHash { get; set; } = null!;
    }
}
