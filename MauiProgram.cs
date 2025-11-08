using MHiker_CrossPlatform.Services;
using MHiker_CrossPlatform.ViewModels;
using MHiker_CrossPlatform.Views;

namespace MHiker_CrossPlatform;

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

        // Đăng ký Service
        builder.Services.AddSingleton<DatabaseService>();

        // Đăng ký ViewModel 
        builder.Services.AddSingleton<HikeListViewModel>();
        builder.Services.AddTransient<AddHikeViewModel>();
        builder.Services.AddTransient<HikeDetailViewModel>(); // THÊM MỚI

        // Đăng ký View
        builder.Services.AddSingleton<HikeListPage>();
        builder.Services.AddTransient<AddHikePage>();
        builder.Services.AddTransient<HikeDetailPage>(); // THÊM MỚI

        return builder.Build();
    }
}