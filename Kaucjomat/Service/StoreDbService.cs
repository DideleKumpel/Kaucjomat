using Kaucjomat.Data;
using Kaucjomat.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaucjomat.Service
{
    class StoreDbService : IStoreDbService
    {
        DatabaseContext _context;
        StoreDbService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Store>> GetStoresAsync()
        {
            var stores = await _context.GetStoresAsync();
            return stores.OrderBy(s => s.Name);
        }

        public async Task<Store> GetStoreByIdAsync(int id)
        {
            var stores = await _context.GetStoresAsync();
            return stores.FirstOrDefault(s => s.Id == id);
        }

        public async Task AddCustomStoreAsync(string storeName)
        {
            if (string.IsNullOrWhiteSpace(storeName))
                throw new ArgumentException("Store name cannot be empty.");

            var newStore = new Store
            {
                Name = storeName.Trim(),
                IsUserDefined = true
            };

            await _context.SaveStoreAsync(newStore);
        }

        public async Task DeleteStoreAsync(int id)
        {
            // to do check if there are any vouchers linked to this store before deleting.
            await _context.DeleteStoreAsync(id);
        }
    }

}
