using Kaucjomat.Model;

namespace Kaucjomat.Service.DbService
{
    public interface IStoreDbService
    {
        Task<IEnumerable<Store>> GetStoresAsync();
        Task<Store> GetStoreByIdAsync(int id);
        Task AddCustomStoreAsync(string storeName);
        Task DeleteStoreAsync(int id, bool deleteLinkedActiveVoucheres);
    }
}
