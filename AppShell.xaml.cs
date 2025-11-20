using MHiker_CrossPlatform.Views; // Đừng quên dòng này

namespace MHiker_CrossPlatform;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Đăng ký đường dẫn cho trang AddHikePage
        // Nếu thiếu dòng này => Crash khi điều hướng
        Routing.RegisterRoute(nameof(AddHikePage), typeof(AddHikePage));
    }
}