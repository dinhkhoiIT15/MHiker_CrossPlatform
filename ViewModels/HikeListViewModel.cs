using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MHiker_CrossPlatform.Models;
using MHiker_CrossPlatform.Services;
using MHiker_CrossPlatform.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MHiker_CrossPlatform.ViewModels
{
    public partial class HikeListViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<Hike> Hikes { get; } = new();

        public HikeListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        public async Task LoadHikesAsync()
        {
            var hikes = await _databaseService.GetHikesAsync();
            Hikes.Clear();
            foreach (var hike in hikes)
                Hikes.Add(hike);
        }

        [RelayCommand]
        async Task GoToAddHikeAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddHikePage)}?HikeId=0");
        }

        [RelayCommand]
        async Task GoToEditHikeAsync(Hike hike)
        {
            if (hike == null) return;
            await Shell.Current.GoToAsync($"{nameof(AddHikePage)}?HikeId={hike.Id}");
        }

        [RelayCommand]
        async Task DeleteHikeAsync(Hike hike)
        {
            if (hike == null) return;

            // Dialog xác nhận Xóa
            bool confirm = await Shell.Current.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to permanently delete the hike '{hike.Name}'?",
                "Yes",
                "No");

            if (confirm)
            {
                await _databaseService.DeleteHikeAsync(hike);

                // Cập nhật lại danh sách trên giao diện ngay lập tức
                Hikes.Remove(hike);

                // (Tùy chọn) Thông báo đã xóa thành công
                // await Shell.Current.DisplayAlert("Deleted", $"Hike '{hike.Name}' has been removed.", "OK");
            }
        }
    }
}