using System.ComponentModel.DataAnnotations;

namespace ASPNET_HHRR_Vacations.Models
{
    public class VacationTicketMetadata
    {
        [Key]
        public int TicketId { get; set; }
    }
}
