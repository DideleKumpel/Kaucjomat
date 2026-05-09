using CommunityToolkit.Maui;
using Kaucjomat.Data;
using Kaucjomat.Service;
using Kaucjomat.Service.DbService;
using Kaucjomat.View;
using Kaucjomat.ViewModel;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;

namespace Kaucjomat
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "EcoWallet.db3");

            builder.Services.AddSingleton<DatabaseContext>(s =>
                ActivatorUtilities.CreateInstance<DatabaseContext>(s, dbPath));

            //SERVICES
            builder.Services.AddSingleton<IVoucherDbService, VoucherDbService>();
            builder.Services.AddSingleton<IStoreDbService, StoreDbService>();


            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<HomeViewModel>();

            builder.Services.AddTransient<VouchersView>();
            builder.Services.AddTransient<VouchersViewModel>();

            builder.Services.AddTransient<VouchersArchivesView>();
            builder.Services.AddTransient<VouchersArchivesViewModel>();

            builder.Services.AddTransient<AddNewViewModel>();
            builder.Services.AddTransient<AddNewView>();

            builder.Services.AddTransient<BarcodeScannerView>();
            

            builder.Services.AddSingleton<AppShell>();

            return builder.Build();
        }
    }
}
