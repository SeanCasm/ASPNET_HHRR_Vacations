using ASPNET_HHRR_Vacations.Models;

namespace ASPNET_HHRR_Vacations.Services
{
    public class Repository
    {
        protected readonly EnterpriseContext _enterpriseContext;
        public Repository(EnterpriseContext enterpriseContext)
        {
            _enterpriseContext = enterpriseContext;
        }
        public async Task Save()
        {
            await _enterpriseContext.SaveChangesAsync();
        }
    }
}
