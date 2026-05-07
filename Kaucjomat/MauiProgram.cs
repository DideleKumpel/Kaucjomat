using Kaucjomat.Data;
using Kaucjomat.Service;
using Microsoft.Extensions.Logging;

namespace Kaucjomat
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
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

            //VIEW
            builder.Services.AddTransient<MainPage>();

            //VIEWMODEL


            return builder.Build();
        }
    }
}
