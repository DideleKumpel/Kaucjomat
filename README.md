================================================================================
  KAUCJOMAT
================================================================================

  A mobile application for storing and tracking deposit machine vouchers.
  Users can manage vouchers from various stores, monitor their value and
  expiry dates, and scan barcodes — eliminating the need to keep paper
  receipts.

  The name "Kaucjomat" is a blend of the Polish words "kaucja" (deposit)
  and "automat" (machine/vending machine).

================================================================================
  TECH STACK
================================================================================

  - .NET MAUI .............. Cross-platform mobile framework (Android / iOS)
  - C# ..................... Programming language
  - SQLite ................. Local on-device database
  - MVVM ................... Architectural pattern (Model-View-ViewModel)
  - CommunityToolkit.Mvvm .. MVVM with source generators
  - CommunityToolkit.Maui .. UI components including Popups

================================================================================
  SCREENS
================================================================================

  HOME
  ----
  Dashboard with an overview of active vouchers:
  - Total value of active vouchers (in PLN)
  - Number of active vouchers
  - Nearest expiry date (color-coded urgency indicator)
  - Total amount saved from already used vouchers
  - Horizontal scroll list of vouchers grouped by store

  VOUCHERS
  --------
  List of active (unused and not expired) vouchers:
  - Filter by store (or "All" view)
  - Sorted by expiry date
  - Display voucher barcode in a popup
  - Mark a voucher as used
  - Navigate to voucher archive

  ADD NEW
  -------
  Form for adding a new voucher:
  - Select a store from the list
  - Enter the voucher amount
  - Set the expiry date
  - Scan barcode using the device camera
  - Full field validation with error messages

  STORES
  ------
  Store list management:
  - View all stores (default and user-defined)
  - Add custom stores
  - Delete stores with confirmation (also removes linked vouchers)
  - Store counter

================================================================================
  ARCHITECTURE
================================================================================

  The project follows the MVVM pattern with a clear separation of concerns.

  Kaucjomat/
  |-- View/                  XAML UI files
  |   |-- HomeView.xaml
  |   |-- VouchersView.xaml
  |   |-- VouchersArchivesView.xaml
  |   |-- AddNewView.xaml
  |   |-- ShopsView.xaml
  |   |-- BarcodeScannerView.xaml
  |   +-- Popup/             Dialogs (Message, Barcode, Confirmation)
  |-- ViewModel/             View logic
  |   |-- HomeViewModel.cs
  |   |-- VouchersViewModel.cs
  |   |-- VouchersArchivesViewModel.cs
  |   |-- AddNewViewModel.cs
  |   +-- ShopsViewModel.cs
  |-- Model/                 Data models (Voucher, Store, StoreActiveVoucherSummary)
  |-- Service/
  |   +-- DbService/         Data access layer
  |       |-- IVoucherDbService.cs
  |       |-- IStoreDbService.cs
  |       |-- VoucherDbService.cs
  |       +-- StoreDbService.cs
  +-- Data/
      +-- DatabaseContext.cs SQLite context

  Data Layer
  ----------
  DatabaseContext manages the SQLite connection and initializes tables on
  first launch. If no stores exist, it seeds the database with default
  Polish grocery chains: Biedronka, Lidl, Kaufland, Netto.

  Services (VoucherDbService, StoreDbService) implement interfaces and serve
  as the only data access layer — ViewModels never interact with the database
  directly.

  Cross-View Communication
  ------------------------
  Data is passed between views using WeakReferenceMessenger from
  CommunityToolkit.Mvvm, keeping components loosely coupled.

================================================================================
  DATA MODELS
================================================================================

  Voucher
  -------
  - Id ................. int       Primary key
  - StoreId ............ int       Associated store
  - Amount ............. decimal   Voucher value (PLN)
  - BarcodeValue ....... string    Barcode content
  - ExpiryDate ......... DateTime  Expiry date
  - IsUsed ............. bool      Whether the voucher has been used

  Store
  -----
  - Id ................. int       Primary key
  - Name ............... string    Store name
  - IsUserDefined ...... bool      Whether the store was added by the user

================================================================================
  FEATURES
================================================================================

  [x] Add vouchers with camera barcode scanning
  [x] Browse active vouchers with per-store filtering
  [x] Voucher archive (used and expired)
  [x] Restore a voucher from archive back to active
  [x] Display voucher barcode in a popup
  [x] Financial summary dashboard
  [x] Store management (add / delete)
  [x] Default list of popular grocery chains
  [x] Form validation with error messages
  [x] Local database -- works fully offline

================================================================================
  GETTING STARTED
================================================================================

  Prerequisites
  -------------
  - .NET 8 SDK or newer
  - Visual Studio 2022+ with the .NET MAUI workload installed
  - Android SDK (for Android emulator/device) or Xcode (for iOS)

  Steps
  -----
  1. Clone the repository
       git clone https://github.com/DideleKumpel/Kaucjomat.git
       cd Kaucjomat

  2. Restore dependencies
       dotnet restore

  3. Run on Android emulator
       dotnet build -t:Run -f net8.0-android

  Alternatively, open the .sln file in Visual Studio and run directly
  from the IDE by selecting your target platform.

================================================================================
  LICENSE
================================================================================

  This project is available under the MIT License.
  See the LICENSE file for details.




================================================================================
  KAUCJOMAT
================================================================================

  Aplikacja mobilna do zapisywania i sledzenia voucherow z kaucjomatow.
  Pozwala uzytkownikowi zarzadzac voucherami ze sklepow, monitorowac ich
  wartosc i daty waznosci oraz skanowac kody kreskowe bez koniecznosci
  trzymania papierowych paragonow.

================================================================================
  TECHNOLOGIE
================================================================================

  - .NET MAUI .............. Framework do budowania aplikacji mobilnych
  - C# ..................... Jezyk programowania
  - SQLite ................. Lokalna baza danych na urzadzeniu
  - MVVM ................... Wzorzec architektoniczny (Model-View-ViewModel)
  - CommunityToolkit.Mvvm .. Implementacja MVVM z generowanym kodem
  - CommunityToolkit.Maui .. Komponenty UI, m.in. Popupy

================================================================================
  WIDOKI
================================================================================

  HOME
  ----
  Ekran glowny z podsumowaniem aktywnych voucherow:
  - Laczna wartosc srodkow z aktywnych voucherow (w PLN)
  - Liczba aktywnych voucherow
  - Data najblizszego wygasniecia (z kolorystycznym sygnalizowaniem pilnosci)
  - Laczna kwota zaoszczedzona z juz wykorzystanych voucherow
  - Poziomy przeglad voucherow pogrupowanych wedlug sklepu

  VOUCHERY
  --------
  Lista aktywnych voucherow (nieuzytych i nieprzeterminowanych):
  - Filtrowanie po sklepie (lub widok "Wszystkie")
  - Sortowanie wedlug daty waznosci
  - Wyswietlenie kodu kreskowego vouchera (popup)
  - Oznaczenie vouchera jako wykorzystanego
  - Przejscie do archiwum voucherow

  DODAJ
  -----
  Formularz dodawania nowego vouchera:
  - Wybor sklepu z listy
  - Wpisanie kwoty
  - Ustawienie daty waznosci
  - Skanowanie kodu kreskowego aparatem
  - Walidacja wszystkich pol z komunikatami bledow

  SKLEPY
  ------
  Zarzadzanie lista sklepow:
  - Lista wszystkich sklepow (domyslnych i uzytkownika)
  - Dodawanie wlasnych sklepow
  - Usuwanie sklepow z potwierdzeniem (kasuje tez powiazane vouchery)
  - Licznik zarejestrowanych sklepow

================================================================================
  ARCHITEKTURA
================================================================================

  Projekt stosuje wzorzec MVVM z wyraznym podzialem odpowiedzialnosci.

  Kaucjomat/
  |-- View/                  Pliki XAML (UI)
  |   |-- HomeView.xaml
  |   |-- VouchersView.xaml
  |   |-- VouchersArchivesView.xaml
  |   |-- AddNewView.xaml
  |   |-- ShopsView.xaml
  |   |-- BarcodeScannerView.xaml
  |   +-- Popup/             Okna dialogowe (Message, Barcode, Confirmation)
  |-- ViewModel/             Logika widokow
  |   |-- HomeViewModel.cs
  |   |-- VouchersViewModel.cs
  |   |-- VouchersArchivesViewModel.cs
  |   |-- AddNewViewModel.cs
  |   +-- ShopsViewModel.cs
  |-- Model/                 Modele danych (Voucher, Store, StoreActiveVoucherSummary)
  |-- Service/
  |   +-- DbService/         Warstwa dostepu do danych
  |       |-- IVoucherDbService.cs
  |       |-- IStoreDbService.cs
  |       |-- VoucherDbService.cs
  |       +-- StoreDbService.cs
  +-- Data/
      +-- DatabaseContext.cs Kontekst SQLite

  Warstwa danych
  --------------
  DatabaseContext zarzadza polaczeniem z SQLite i inicjalizuje tabele przy
  pierwszym uruchomieniu. Przy braku sklepow automatycznie seeduje baze
  domyslnymi sieciami handlowymi: Biedronka, Lidl, Kaufland, Netto.

  Serwisy (VoucherDbService, StoreDbService) implementuja interfejsy i
  stanowia jedyna warstwe komunikacji z baza -- ViewModele nie dotykaja
  bazy bezposrednio.

  Komunikacja miedzy widokami
  ---------------------------
  Do przekazywania danych miedzy widokami uzywany jest WeakReferenceMessenger
  z CommunityToolkit.Mvvm, co zapewnia luznie powiazanie komponentow.

================================================================================
  MODEL DANYCH
================================================================================

  Voucher
  -------
  - Id ................. int       Klucz glowny
  - StoreId ............ int       Powiazany sklep
  - Amount ............. decimal   Wartosc vouchera (PLN)
  - BarcodeValue ....... string    Wartosc kodu kreskowego
  - ExpiryDate ......... DateTime  Data waznosci
  - IsUsed ............. bool      Czy voucher zostal wykorzystany

  Store
  -----
  - Id ................. int       Klucz glowny
  - Name ............... string    Nazwa sklepu
  - IsUserDefined ...... bool      Czy sklep dodany przez uzytkownika

================================================================================
  FUNKCJONALNOSCI
================================================================================

  [x] Dodawanie voucherow ze skanowaniem kodu kreskowego
  [x] Przegladanie aktywnych voucherow z filtrowaniem po sklepie
  [x] Archiwum voucherow (wykorzystane i przeterminowane)
  [x] Przywracanie vouchera z archiwum do aktywnych
  [x] Wyswietlanie kodu kreskowego vouchera w popupie
  [x] Dashboard z podsumowaniem finansowym
  [x] Zarzadzanie lista sklepow (dodawanie / usuwanie)
  [x] Domyslna lista popularnych sieci handlowych
  [x] Walidacja formularzy z komunikatami bledow
  [x] Lokalna baza danych -- dziala offline

================================================================================
  URUCHOMIENIE
================================================================================

  Wymagania
  ---------
  - .NET 8 SDK lub nowszy
  - Visual Studio 2022+ z zainstalowanym pakietem .NET MAUI
  - Android SDK (dla urzadzen/emulatorow Android) lub Xcode (dla iOS)

  Kroki
  -----
  1. Sklonuj repozytorium
       git clone https://github.com/DideleKumpel/Kaucjomat.git
       cd Kaucjomat

  2. Przywroc zaleznosci
       dotnet restore

  3. Uruchom na emulatorze Android
       dotnet build -t:Run -f net8.0-android

  Mozna rowniez otworzyc plik .sln w Visual Studio i uruchomic bezposrednio
  z IDE, wybierajac docelowa platforme.

================================================================================
  LICENCJA
================================================================================

  Projekt dostepny na licencji MIT.
  Szczegoly w pliku LICENSE.

================================================================================
