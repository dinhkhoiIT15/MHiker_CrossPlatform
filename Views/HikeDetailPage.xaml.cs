using MHiker_CrossPlatform.ViewModels;

namespace MHiker_CrossPlatform.Views;

public partial class HikeDetailPage : ContentPage
{
    public HikeDetailPage(HikeDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // T?i l?i d? li?u khi trang xu?t hi?n (sau khi S?a)
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HikeDetailViewModel vm)
        {
            // T?i l?i d? li?u cho ID hi?n t?i
            vm.LoadHikeAsync(vm.HikeId);
        }
    }
}