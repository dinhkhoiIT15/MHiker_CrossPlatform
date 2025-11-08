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
            // --- VỊ TRÍ SỬA ---: Truyền ID=0 để báo hiệu đây là chế độ Add
            await Shell.Current.GoToAsync($"{nameof(AddHikePage)}?HikeId=0");
        }

        // --- VỊ TRÍ SỬA ---: Thêm Command để điều hướng đến trang Edit
        [RelayCommand]
        private async Task GoToEditHikeAsync(Hike hike)
        {
            if (hike == null) return;
            // Truyền ID của hike cần edit qua trang AddHikePage
            await Shell.Current.GoToAsync($"{nameof(AddHikePage)}?HikeId={hike.Id}");
        }


        [RelayCommand]
        private async Task DeleteHikeAsync(Hike hike)
        {
            if (hike == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Delete Hike", $"Are you sure you want to delete '{hike.Name}'?", "Yes", "No");
            if (confirm)
            {
                await _databaseService.DeleteHikeAsync(hike);
                Hikes.Remove(hike);
            }
        }
    }
}
