using Kaucjomat.Data;
using Kaucjomat.Model;

namespace Kaucjomat.Service.DbService
{
    public class VoucherDbService : IVoucherDbService
    {
        private readonly DatabaseContext _context;

        public VoucherDbService(DatabaseContext context)
        {
            _context = context;
        }

        // --- READ OPERATIONS ---

        public async Task<IEnumerable<Voucher>> GetAllVouchersAsync()
        {
            return await _context.GetVouchersAsync();
        }

        public async Task<IEnumerable<Voucher>> GetActiveVouchersAsync()
        {
            var vouchers = await _context.GetVouchersAsync();
            return vouchers.Where(v => !v.IsUsed && v.ExpiryDate >= DateTime.Today)
                           .OrderBy(v => v.ExpiryDate);
        }

        public async Task<IEnumerable<Voucher>> GetArchivedVouchersAsync()
        {
            var vouchers = await _context.GetVouchersAsync();
            // Return used vouchers or already expired ones
            return vouchers.Where(v => v.IsUsed || v.ExpiryDate < DateTime.Today)
                           .OrderBy(v => v.ExpiryDate);
        }

        public async Task<IEnumerable<Voucher>> GetVouchersByStoreAsync(int storeId)
        {
            var vouchers = await _context.GetVouchersAsync();
            return vouchers.Where(v => v.StoreId == storeId && !v.IsUsed)
                           .OrderBy(v => v.ExpiryDate);
        }

        public async Task<IEnumerable<Voucher>> GetArchivedVouchersByStoreAsync(int storeId)
        {
            var vouchers = await _context.GetVouchersAsync();
            return vouchers.Where(v => v.StoreId == storeId && (v.IsUsed || v.ExpiryDate < DateTime.Today))
                           .OrderBy(v => v.ExpiryDate);
        }


        public async Task AddVoucherAsync(Voucher voucher)
        {
            if (voucher.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
            if (voucher.ExpiryDate < DateTime.Today)
                throw new ArgumentException("Expiry date cannot be in the past.");

            await _context.SaveVoucherAsync(voucher);
        }

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            await _context.SaveVoucherAsync(voucher);
        }

        public async Task ToggleVoucherStatusAsync(int voucherId, bool isUsed)
        {
            var vouchers = await _context.GetVouchersAsync();
            var voucher = vouchers.FirstOrDefault(v => v.Id == voucherId);

            if (voucher != null)
            {
                voucher.IsUsed = isUsed;
                await _context.SaveVoucherAsync(voucher);
            }
        }

        public async Task DeleteVoucherAsync(int voucherId)
        {
            await _context.DeleteVoucherAsync(voucherId);
        }

        public async Task<decimal> GetTotalActiveAmountAsync()
        {
            var active = await GetActiveVouchersAsync();
            return active.Sum(v => v.Amount);
        }

        public async Task<decimal> GetTotalSavedAmountAsync()
        {
            var vouchers = await _context.GetVouchersAsync();
            return vouchers.Where(v => v.IsUsed).Sum(v => v.Amount);
        }

        public async Task<int> GetActiveCountAsync()
        {
            var active = await GetActiveVouchersAsync();
            return active.Count();
        }

        public async Task<Voucher> GetClosestExpiryVoucherAsync()
        {
            var active = await GetActiveVouchersAsync();
            return active.OrderBy(v => v.ExpiryDate).FirstOrDefault();
        }

        public async Task<List<StoreActiveVoucherSummary>> GetActiveVouchersSummaryAsync()
        {
            var vouchers = await GetActiveVouchersAsync();
            var stores = await _context.GetStoresAsync();

            var storeDict = stores.ToDictionary(s => s.Id, s => s.Name);

            var summary = vouchers
                .Where(v => storeDict.ContainsKey(v.StoreId))
                .GroupBy(v => v.StoreId)
                .Select(group => new StoreActiveVoucherSummary
                {
                    StoreName = storeDict[group.Key],
                    ActiveVouchersCount = group.Count()
                })
                .OrderByDescending(res => res.ActiveVouchersCount)
                .ToList();

            return summary;
        }
    }
}
