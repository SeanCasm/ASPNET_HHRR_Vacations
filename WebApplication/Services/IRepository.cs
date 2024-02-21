namespace ASPNET_HHRR_Vacations.Services
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindById(int id);
        Task Delete(T entity);

    }
}
