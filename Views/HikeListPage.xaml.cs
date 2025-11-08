// --- V? TRÍ S?A ---: T?o file m?i hoàn toàn
using MHiker_CrossPlatform.ViewModels;

namespace MHiker_CrossPlatform.Views;

public partial class HikeListPage : ContentPage
{
    private readonly HikeListViewModel _viewModel;

    public HikeListPage(HikeListViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    // T? ??ng t?i l?i danh sách khi trang xu?t hi?n
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadHikesCommand.ExecuteAsync(null);
    }
}