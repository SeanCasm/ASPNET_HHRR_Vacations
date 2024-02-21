namespace ASPNET_HHRR_Vacations.Models
{
    public class PasswordChangeDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
