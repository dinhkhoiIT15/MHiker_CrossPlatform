using MHiker_CrossPlatform.Views;

namespace MHiker_CrossPlatform;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Đăng ký các route
        Routing.RegisterRoute(nameof(AddHikePage), typeof(AddHikePage));
        Routing.RegisterRoute(nameof(HikeDetailPage), typeof(HikeDetailPage)); // THÊM MỚI
    }
}