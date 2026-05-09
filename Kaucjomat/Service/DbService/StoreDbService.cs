using Kaucjomat.Data;
using Kaucjomat.Model;

namespace Kaucjomat.Service.DbService
{
    public class StoreDbService : IStoreDbService
    {
        DatabaseContext _context;
        public StoreDbService(DatabaseContext context)
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

        public async Task DeleteStoreAsync(int id, bool deleteLinkedActiveVoucheres)
        {
            var vouchers = await _context.GetVouchersAsync();
            var vouchersBySotre = vouchers.Where(v => v.StoreId == id);
            if (!deleteLinkedActiveVoucheres && vouchersBySotre.Any(v => !v.IsUsed))
            {
                throw new InvalidOperationException("Shop has active vouchers.");
            }
            foreach (Voucher voucher in vouchersBySotre)
            {
                await _context.DeleteVoucherAsync(voucher.Id);
            }
            await _context.DeleteStoreAsync(id);
        }
    }

}
