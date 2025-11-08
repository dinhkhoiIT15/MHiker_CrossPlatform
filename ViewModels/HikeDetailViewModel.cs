using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MHiker_CrossPlatform.Models;
using MHiker_CrossPlatform.Services;
using MHiker_CrossPlatform.Views;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MHiker_CrossPlatform.ViewModels
{
    [QueryProperty(nameof(HikeId), "id")]
    public partial class HikeDetailViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public HikeDetailViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [ObservableProperty]
        private Hike _hike;

        private int _hikeId;
        public int HikeId
        {
            get => _hikeId;
            set
            {
                SetProperty(ref _hikeId, value);
                LoadHikeAsync(value);
            }
        }

        public async void LoadHikeAsync(int hikeId)
        {
            try
            {
                Hike = await _databaseService.GetHikeByIdAsync(hikeId);
            }
            catch (Exception ex)
            {
                // THAY ĐỔI: Chuyển sang Tiếng Anh
                Debug.WriteLine($"Failed to load Hike: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task DeleteHike()
        {
            if (Hike == null) return;

            // THAY ĐỔI: Chuyển sang Tiếng Anh
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete '{Hike.Name}'? This action cannot be undone.",
                "Delete",
                "Cancel");

            if (confirm)
            {
                await _databaseService.DeleteHikeAsync(Hike);
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task EditHike()
        {
            if (Hike == null) return;

            // Điều hướng đến AddHikePage và gửi ID để Sửa
            await Shell.Current.GoToAsync($"{nameof(AddHikePage)}?id={Hike.Id}");
        }
    }
}