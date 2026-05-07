using Kaucjomat.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaucjomat.Service
{
    interface IStoreDbService
    {
        Task<IEnumerable<Store>> GetStoresAsync();
        Task<Store> GetStoreByIdAsync(int id);
        Task AddCustomStoreAsync(string storeName);
        Task DeleteStoreAsync(int id);
    }
}
