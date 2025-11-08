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
        private async Task GoToAddHike()
        {
            await Shell.Current.GoToAsync(nameof(AddHikePage));
        }

        [RelayCommand]
        public async Task GetHikes() // Đổi thành public để View gọi
        {
            Hikes.Clear();
            var hikes = await _databaseService.GetHikesAsync();
            foreach (var hike in hikes)
            {
                Hikes.Add(hike);
            }
        }

        [RelayCommand]
        private async Task SelectHike(Hike hike)
        {
            if (hike == null)
                return;

            // Điều hướng đến trang chi tiết, gửi ID qua QueryProperty
            await Shell.Current.GoToAsync($"{nameof(HikeDetailPage)}?id={hike.Id}");
        }
    }
}