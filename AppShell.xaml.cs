// --- VỊ TRÍ SỬA ---: Thêm 'using' cho thư mục Views
using MHiker_CrossPlatform.Views;

namespace MHiker_CrossPlatform;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // --- VỊ TRÍ SỬA ---: Đăng ký route cho trang AddHikePage
        Routing.RegisterRoute(nameof(AddHikePage), typeof(AddHikePage));
    }
}