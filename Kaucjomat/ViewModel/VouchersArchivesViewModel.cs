using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kaucjomat.Model;
using Kaucjomat.Service.DbService;
using Kaucjomat.View.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Kaucjomat.ViewModel
{
    public partial class VouchersArchivesViewModel: ObservableObject
    {
        private readonly IVoucherDbService _voucherDbService;
        private readonly IStoreDbService _storeDbService;

        public VouchersArchivesViewModel(IVoucherDbService voucherDbService, IStoreDbService storeDbService)
        {
            _voucherDbService = voucherDbService;
            _storeDbService = storeDbService;


            LoadStoresCommand.Execute(null);
            LoadVouchersByStoreCommand.Execute(null);
        }

        [ObservableProperty]
        private ObservableCollection<Voucher> _vouchers;

        [ObservableProperty]
        private ObservableCollection<Store> _stores;

        [ObservableProperty]
        private Store _selectedStore;

        partial void OnSelectedStoreChanged(Store value)
        {
            LoadVouchersByStoreCommand.Execute(null);
        }


        [RelayCommand]
        private async Task LoadStores()
        {
            var stores = await _storeDbService.GetStoresAsync();
            Stores = new ObservableCollection<Store>(stores);
            Stores.Insert(0, new Store() { Id = 0, Name = "Wszystkie" });
            SelectedStore = Stores[0];
        }

        [RelayCommand]
        private async Task LoadVouchersByStore()
        {
            var vouchers = await _voucherDbService.GetArchivedVouchersAsync();

            if (SelectedStore != null && SelectedStore.Id != 0)
            {
                var filtered = vouchers.Where(v => v.StoreId == SelectedStore.Id).ToList();
                vouchers = filtered;
            }


            Vouchers = new ObservableCollection<Voucher>(vouchers.OrderBy(v => v.ExpiryDate));
        }

        [RelayCommand]
        private async void ChangeVoucherStatusToUnused(Voucher voucher)
        {
            if (voucher == null)
                return;
            voucher.IsUsed = false;
            await _voucherDbService.UpdateVoucherAsync(voucher);
            await LoadVouchersByStore();
        }

        [RelayCommand]
        private void ShowVoucherCode(Voucher voucher)
        {
            if (voucher == null) return;

            var popup = new BarcodePopup(voucher);

            Application.Current.MainPage.ShowPopup(popup);
        }

        [RelayCommand]
        private async Task GoToVouchers()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task RefreshData()
        {
            await LoadStores();
            await LoadVouchersByStore();
        }
    }
}
