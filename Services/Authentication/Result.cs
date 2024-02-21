namespace ASPNET_HHRR_Vacations.Services.Authentication
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; } = false;
        public string? ErrorMessage { get; set; } = null;
        public T? ObjectResult { get; set; }
    }
}
