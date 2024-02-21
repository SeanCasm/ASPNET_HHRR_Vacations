namespace ASPNET_HHRR_Vacations.Helpers
{
    public static class TicketRequestStatus
    {
        public static readonly int sent = 1;
        public static readonly int approved = 2;
        public static readonly int declined = 3;
        static readonly string sentStyle = "warning";
        static readonly string approvedStyle = "success";
        static readonly string declinedStyle = "danger";
        public static string GetTicketStatusStyle(int ticketStatus = 1)
        {
            string styleClass = sentStyle;
            switch (ticketStatus)
            {
                case 1:
                    styleClass = sentStyle;
                    break;
                case 2:
                    styleClass = approvedStyle;
                    break;
                case 3:
                    styleClass = declinedStyle;
                    break;
            }
            return styleClass;
        }
    }
}
