using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_HHRR_Vacations.Models
{
    public class VacationMetadata
    {
        [Key]
        public int VacationId { get; set; }
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }
    }
}