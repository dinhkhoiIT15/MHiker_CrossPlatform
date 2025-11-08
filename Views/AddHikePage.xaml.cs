// --- VỊ TRÍ SỬA ---: Thêm 'using' cho thư mục ViewModels
using MHiker_CrossPlatform.ViewModels;

namespace MHiker_CrossPlatform.Views;

public partial class AddHikePage : ContentPage
{
    // --- VỊ TRÍ SỬA ---: Thay thế toàn bộ constructor cũ bằng constructor mới này
    public AddHikePage(AddHikeViewModel viewModel)
    {
        InitializeComponent();

        // Gán ViewModel được "tiêm" vào làm BindingContext cho trang
        BindingContext = viewModel;
    }
}
