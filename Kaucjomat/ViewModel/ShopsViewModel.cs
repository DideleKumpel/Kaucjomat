using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kaucjomat.Model;
using Kaucjomat.Service.DbService;
using Kaucjomat.View.Popup;
using System.Collections.ObjectModel;


namespace Kaucjomat.ViewModel
{
    public partial class ShopsViewModel : ObservableObject
    {
        private readonly IStoreDbService _storeDbService;

        [ObservableProperty]
        private string _storeName;

        [ObservableProperty]
        private int _shopsCount;

        [ObservableProperty]
        private ObservableCollection<Store> _storeList;


        public ShopsViewModel(IStoreDbService storeDbService)
        {
            _storeDbService = storeDbService;
            GetStores();
        }

        private async Task GetStores()
        {
            var stores = await _storeDbService.GetStoresAsync();
            StoreList = new ObservableCollection<Store>(stores);
            await CountShops();
        }

        private async Task CountShops()
        {
            ShopsCount = StoreList.Count();
        }

        [RelayCommand]
        private async Task AddStore()
        {
            if (string.IsNullOrWhiteSpace(StoreName))
                return;
            await _storeDbService.AddCustomStoreAsync(StoreName);
            StoreName = string.Empty;
            await GetStores();
        }

        [RelayCommand]
        private async Task DeleteStore(Store store) 
        {
            if (store == null)
                return;
            var confirmationPopup = new ConformationPopUp("Potwierdzenie", "Czy na pewno chcesz usunąć sklep? \nSpowoduje to usunięcie wszystich voucherów do tego skelpu");
            await App.Current.MainPage.ShowPopupAsync(confirmationPopup);
            if (confirmationPopup.Result)
            {
                await _storeDbService.DeleteStoreAsync(store.Id, true);
                await GetStores();
            }
        }
    }
}
