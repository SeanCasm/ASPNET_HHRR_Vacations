using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASPNET_HHRR_Vacations.Models
{
    public class EmployeeMetadata
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name field requires letters only.")]
        [DisplayName("First name")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name field requires letters only.")]
        [DisplayName("Last name")]
        public string LastName { get; set; } = null!;
    }
}
