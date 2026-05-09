using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kaucjomat.Messages;
using Kaucjomat.Model;
using Kaucjomat.Service.DbService;
using Kaucjomat.View.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Kaucjomat.ViewModel
{
    public partial class AddNewViewModel: ObservableObject, IRecipient<BarcodeScannedMessage>
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

            WeakReferenceMessenger.Default.RegisterAll(this);
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
                var popup = new MessagePopup("Bład", "Wybierz sklep");
                Application.Current.MainPage.ShowPopup(popup);
                return;
            }
            else if (Amount <= 0)
            {
                var popup = new MessagePopup("Bład", "Dodaj kwotę");
                Application.Current.MainPage.ShowPopup(popup);
                return;
            }else if (ExpiryDate < DateTime.Now)
            {
                var popup = new MessagePopup("Bład", "Voucher jest już przeterminowany");
                Application.Current.MainPage.ShowPopup(popup);
                return;
            }
            else if(string.IsNullOrWhiteSpace(BarcodeValue))
            {
                var popup = new MessagePopup("Bład", "Wypełnij kod kreskowy");
                Application.Current.MainPage.ShowPopup(popup);
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

            try
            {
                await _voucherService.AddVoucherAsync(newVoucher);
                var popup = new MessagePopup("Sukces", "Voucher został dodany");
                Application.Current.MainPage.ShowPopup(popup);
            }
            catch (Exception ex) {
                var popup = new MessagePopup("Bład", $"Nie można dodać vouchera: {ex.Message}");
                Application.Current.MainPage.ShowPopup(popup);
            }
        }

        [RelayCommand]
        private async Task ReadBarcodeFromCamera()
        {
            await Shell.Current.GoToAsync("BarcodeScannerView");
        }

        public void Receive(BarcodeScannedMessage message)
        {
            BarcodeValue = message.Value;
        }
    }
}
