// --- VỊ TRÍ SỬA ---: Thêm 'using' cho các thư mục mới
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


        // Đăng ký DatabaseService là Singleton (chỉ có 1 thể hiện duy nhất)
        builder.Services.AddSingleton<DatabaseService>();

        // Đăng ký ViewModels
        builder.Services.AddTransient<AddHikeViewModel>();
        builder.Services.AddTransient<HikeListViewModel>();

        // Đăng ký Pages (Views)
        builder.Services.AddTransient<AddHikePage>();
        builder.Services.AddTransient<HikeListPage>();


        return builder.Build();
    }
}