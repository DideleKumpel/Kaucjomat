using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kaucjomat.Model;
using Kaucjomat.Service;
using Kaucjomat.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Kaucjomat.ViewModel
{
    public partial class AddNewViewModel: ObservableObject
    {
        private readonly IVoucherDbService _voucherService;
        private readonly IStoreDbService _storeService;

        [ObservableProperty]
        private ObservableCollection<Store> _stores = new();

        [ObservableProperty]
        private Store _selectedStore;

        [ObservableProperty]
        private  double _amount;

        [ObservableProperty]
        private DateTime _expiryDate = DateTime.Now.AddMonths(1);

        [ObservableProperty]
        private string _barcodeValue;

        public AddNewViewModel(IVoucherDbService voucherService, IStoreDbService storeService)
        {
            _voucherService = voucherService;
            _storeService = storeService;
            LoadStoresCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadStoresAsync()
        {
            var stores = await _storeService.GetStoresAsync();
            Stores = new ObservableCollection<Store>(stores);
        }

        [RelayCommand]
        private async Task SaveVoucher()
        {
            if (SelectedStore == null)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Wybierz sklep", "OK");
                return;
            }else if(string.IsNullOrWhiteSpace(BarcodeValue))
            {
                await Shell.Current.DisplayAlertAsync("Error", "Wypełnij kod kreskowy", "OK");
                return;
            }
            else if(Amount <= 0)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Dodaj kwote", "OK");
                return;
            }

            var newVoucher = new Voucher
            {
                StoreId = SelectedStore.Id,
                Amount = (decimal)Amount,
                BarcodeValue = BarcodeValue,
                ExpiryDate = ExpiryDate,
                IsUsed = false
            };

            await _voucherService.AddVoucherAsync(newVoucher);
        }

        private async Task ReadBarcodeFromCamera()
        {
            // Implement barcode reading from camera here
            // This is a placeholder for the actual implementation
            await Task.Delay(1000); // Simulate some delay
            BarcodeValue = "1234567890123"; // Example barcode value
        }
    }
}
