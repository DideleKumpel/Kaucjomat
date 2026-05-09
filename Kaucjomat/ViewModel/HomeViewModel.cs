using CommunityToolkit.Mvvm.ComponentModel;
using Kaucjomat.Model;
using Kaucjomat.Service.DbService;
using System.Collections.ObjectModel;


namespace Kaucjomat.ViewModel
{
    public partial class HomeViewModel: ObservableObject
    {
        private readonly IVoucherDbService _voucherDbService;
        private readonly IStoreDbService _storeDbService;

        public HomeViewModel(IVoucherDbService voucherDbService, IStoreDbService storeDbService)
        {
            _voucherDbService = voucherDbService;
            _storeDbService = storeDbService;

            RefreshAll();

        }

        [ObservableProperty]
        private decimal _yourFunds;

        [ObservableProperty]
        private int _activeVouchersCount;

        [ObservableProperty]
        private DateTime _nearestExpirationDate;

        [ObservableProperty]
        private decimal _totalSaved;

        [ObservableProperty]
        private ObservableCollection<StoreActiveVoucherSummary> _storeActiveVoucherSummariesList;

        private async Task RefreshSummaries()
        {
            var summaries = await _voucherDbService.GetActiveVouchersSummaryAsync();
            StoreActiveVoucherSummariesList = new ObservableCollection<StoreActiveVoucherSummary>(summaries);
        }

        private async Task RefreshFunds()
        {
            YourFunds = await _voucherDbService.GetTotalActiveAmountAsync();
        }

        private async Task RefreshSaved()
        {
            TotalSaved = await _voucherDbService.GetTotalSavedAmountAsync();
        }

        private async Task RefreshActiveCount()
        {
            ActiveVouchersCount = await _voucherDbService.GetActiveCountAsync();
        }

        private async Task RefreshNearestExpiration()
        {
            var voucher = await _voucherDbService.GetClosestExpiryVoucherAsync();
            NearestExpirationDate = voucher != null ? voucher.ExpiryDate : DateTime.MaxValue;
        }

        public async Task RefreshAll()
        {
            await RefreshFunds();
            await RefreshSaved();
            await RefreshActiveCount();
            await RefreshNearestExpiration();
            await RefreshSummaries();
        }
    }
}
