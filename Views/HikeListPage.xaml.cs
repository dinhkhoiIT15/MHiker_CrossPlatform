using MHiker_CrossPlatform.ViewModels;

namespace MHiker_CrossPlatform.Views;

public partial class HikeListPage : ContentPage
{
    public HikeListPage(HikeListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // Tải lại dữ liệu mỗi khi trang xuất hiện
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HikeListViewModel vm)
        {
            // Không dùng await ở đây để UI không bị treo
            vm.GetHikesCommand.Execute(null);
        }
    }
}