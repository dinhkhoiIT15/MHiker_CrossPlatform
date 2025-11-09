using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MHiker_CrossPlatform.Models;
using MHiker_CrossPlatform.Services;
using MHiker_CrossPlatform.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MHiker_CrossPlatform.ViewModels
{
    public partial class HikeListViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<Hike> Hikes { get; } = new ObservableCollection<Hike>();

        public HikeListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        private async Task LoadHikesAsync()
        {
            var hikes = await _databaseService.GetHikesAsync();
            Hikes.Clear();
            foreach (var hike in hikes)
            {
                Hikes.Add(hike);
            }
        }

        [RelayCommand]
        private async Task GoToAddHikeAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddHikePage)}?HikeId=0");
        }

        [RelayCommand]
        private async Task GoToEditHikeAsync(Hike hike)
        {
            if (hike == null) return;
            await Shell.Current.GoToAsync($"{nameof(AddHikePage)}?HikeId={hike.Id}");
        }
    }
}