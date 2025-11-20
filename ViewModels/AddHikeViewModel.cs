using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MHiker_CrossPlatform.Models;
using MHiker_CrossPlatform.Services;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MHiker_CrossPlatform.ViewModels
{
    public partial class AddHikeViewModel : ObservableObject, IQueryAttributable
    {
        private readonly DatabaseService _databaseService;
        private int _hikeId;

        public AddHikeViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            DateOfHike = DateTime.Now;
        }

        [ObservableProperty] string name;
        [ObservableProperty] string location;
        [ObservableProperty] DateTime dateOfHike;
        [ObservableProperty] bool parkingAvailable;
        [ObservableProperty] string lengthOfHike;
        [ObservableProperty] string difficultyLevel; // Picker sẽ bind vào đây
        [ObservableProperty] string description;
        [ObservableProperty] string hikerCount;
        [ObservableProperty] string equipment;

        [ObservableProperty]
        private bool _isEditMode;

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("HikeId"))
            {
                _hikeId = int.Parse(query["HikeId"].ToString());
                IsEditMode = _hikeId > 0;

                if (IsEditMode)
                {
                    var hike = await _databaseService.GetHikeAsync(_hikeId);
                    if (hike != null)
                    {
                        Name = hike.Name;
                        Location = hike.Location;
                        DateOfHike = hike.DateOfHike;
                        ParkingAvailable = hike.ParkingAvailable;
                        LengthOfHike = hike.LengthOfHike.ToString();
                        DifficultyLevel = hike.DifficultyLevel; // Load giá trị cũ lên
                        Description = hike.Description;
                        HikerCount = hike.HikerCount.ToString();
                        Equipment = hike.Equipment;
                    }
                }
                else
                {
                    ClearForm();
                }
            }
        }

        private void ClearForm()
        {
            _hikeId = 0;
            IsEditMode = false;
            Name = string.Empty;
            Location = string.Empty;
            DateOfHike = DateTime.Now;
            ParkingAvailable = false;
            LengthOfHike = string.Empty;

            // --- THAY ĐỔI TẠI ĐÂY ---
            // Đặt về null để Picker hiển thị Title "Select Difficulty"
            DifficultyLevel = null;

            Description = string.Empty;
            HikerCount = string.Empty;
            Equipment = string.Empty;
        }

        [RelayCommand]
        async Task SaveHike()
        {
            // 1. Validate dữ liệu
            // Thêm kiểm tra DifficultyLevel có null hay không
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Location) ||
                string.IsNullOrWhiteSpace(LengthOfHike) ||
                string.IsNullOrWhiteSpace(DifficultyLevel)) // Bắt buộc phải chọn Difficulty
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please fill in Name, Location, Length, and select a Difficulty Level.", "OK");
                return;
            }

            if (!double.TryParse(LengthOfHike, out double lengthValue))
            {
                await Shell.Current.DisplayAlert("Validation Error", "Length must be a valid number.", "OK");
                return;
            }

            int.TryParse(HikerCount, out int countValue);
            string parkingStatus = ParkingAvailable ? "Yes" : "No";

            // --- THAY ĐỔI TẠI ĐÂY ---
            // Không cần toán tử ?? "Medium" nữa vì ta đã bắt buộc chọn ở trên
            string difficulty = DifficultyLevel;

            string summaryMessage = $"""
                Please confirm the following details:

                Name: {Name}
                Location: {Location}
                Date: {DateOfHike:dd/MM/yyyy}
                Parking: {parkingStatus}
                Length: {lengthValue} km
                Difficulty: {difficulty}
                Hikers: {countValue}
                Equipment: {Equipment}
                Description: {Description}
                """;

            bool confirm = await Shell.Current.DisplayAlert("Confirm Submission", summaryMessage, "Submit", "Cancel");

            if (!confirm) return;

            var hike = new Hike
            {
                Id = _hikeId,
                Name = Name,
                Location = Location,
                DateOfHike = DateOfHike,
                ParkingAvailable = ParkingAvailable,
                LengthOfHike = lengthValue,
                DifficultyLevel = difficulty,
                Description = Description,
                HikerCount = countValue,
                Equipment = Equipment
            };

            await _databaseService.SaveHikeAsync(hike);
            await Shell.Current.DisplayAlert("Success", "Hike saved successfully.", "OK");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task DeleteHike()
        {
            if (_hikeId == 0) return;

            bool confirm = await Shell.Current.DisplayAlert("Confirm Delete", "Are you sure you want to delete this hike permanently?", "Yes", "No");

            if (confirm)
            {
                var hikeToDelete = new Hike { Id = _hikeId };
                await _databaseService.DeleteHikeAsync(hikeToDelete);
                await Shell.Current.DisplayAlert("Deleted", "Hike deleted successfully.", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}