using MHiker_CrossPlatform.ViewModels;

namespace MHiker_CrossPlatform.Views;

public partial class AddHikePage : ContentPage
{
    public AddHikePage(AddHikeViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}
