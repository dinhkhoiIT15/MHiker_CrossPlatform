using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MHiker_CrossPlatform.Models;
using MHiker_CrossPlatform.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHiker_CrossPlatform.ViewModels
{
    public partial class AddHikeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public AddHikeViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _location;

        // THAY ĐỔI: Từ DateTime sang string
        [ObservableProperty]
        private string _dateOfHike;

        // THAY ĐỔI: Tên (bỏ 'Is')
        [ObservableProperty]
        private bool _parkingAvailable;

        // THAY ĐỔI: Từ double sang string
        [ObservableProperty]
        private string _lengthOfHike;

        [ObservableProperty]
        private string _difficultyLevel;

        [ObservableProperty]
        private string _description;

        // THAY ĐỔI: Từ int sang string
        [ObservableProperty]
        private string _hikerCount;

        // THÊM MỚI
        [ObservableProperty]
        private string _equipment;

        // ĐÃ XÓA: EstimatedTime


        [RelayCommand]
        private async Task SaveHike()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Location) ||
                string.IsNullOrWhiteSpace(DateOfHike) || // Cập nhật kiểm tra
                string.IsNullOrWhiteSpace(LengthOfHike)) // Cập nhật kiểm tra
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng điền các trường bắt buộc", "OK");
                return;
            }

            var newHike = new Hike
            {
                Name = Name,
                Location = Location,
                DateOfHike = DateOfHike, // string
                ParkingAvailable = ParkingAvailable, // bool
                LengthOfHike = LengthOfHike, // string
                DifficultyLevel = DifficultyLevel,
                Description = Description,
                HikerCount = HikerCount, // string
                Equipment = Equipment // THÊM MỚI
            };

            await _databaseService.AddHikeAsync(newHike);
            await Shell.Current.GoToAsync(".."); // Quay lại trang trước
        }
    }
}