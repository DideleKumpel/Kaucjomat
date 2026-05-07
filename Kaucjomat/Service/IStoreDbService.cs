using Kaucjomat.Model;

namespace Kaucjomat.Service
{
    public interface IStoreDbService
    {
        Task<IEnumerable<Store>> GetStoresAsync();
        Task<Store> GetStoreByIdAsync(int id);
        Task AddCustomStoreAsync(string storeName);
        Task DeleteStoreAsync(int id);
    }
}
