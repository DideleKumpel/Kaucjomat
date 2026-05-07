using Kaucjomat.Model;
namespace Kaucjomat.Service
{
    interface IVoucherDbService
    {
        Task<IEnumerable<Voucher>> GetAllVouchersAsync();
        Task<IEnumerable<Voucher>> GetActiveVouchersAsync();
        Task<IEnumerable<Voucher>> GetArchivedVouchersAsync();
        Task<IEnumerable<Voucher>> GetVouchersByStoreAsync(int storeId);

        // CREATE / UPDATE / DELETE
        Task AddVoucherAsync(Voucher voucher);
        Task UpdateVoucherAsync(Voucher voucher);
        Task ToggleVoucherStatusAsync(int voucherId, bool isUsed);
        Task DeleteVoucherAsync(int voucherId);

        // STATISTICS
        Task<decimal> GetTotalActiveAmountAsync();
        Task<decimal> GetTotalSavedAmountAsync();
        Task<int> GetActiveCountAsync();
        Task<Voucher> GetClosestExpiryVoucherAsync();
    }
}
