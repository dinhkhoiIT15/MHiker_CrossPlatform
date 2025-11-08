using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MHiker_CrossPlatform.Models;
using MHiker_CrossPlatform.Services;
using System; // THÊM MỚI
using System.Diagnostics;
using System.Threading.Tasks;

namespace MHiker_CrossPlatform.ViewModels
{
    [QueryProperty(nameof(HikeId), "id")]
    public partial class AddHikeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private int _currentHikeId = 0;

        public AddHikeViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Title = "Add New Hike";
            _dateOfHike = DateTime.Now; // Khởi tạo ngày
        }

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _location;

        // THAY ĐỔI: Sang DateTime
        [ObservableProperty]
        private DateTime _dateOfHike;

        [ObservableProperty]
        private bool _parkingAvailable;

        [ObservableProperty]
        private string _lengthOfHike;

        [ObservableProperty]
        private string _difficultyLevel;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _hikerCount;

        [ObservableProperty]
        private string _equipment;

        public int HikeId
        {
            set
            {
                if (value > 0)
                {
                    _currentHikeId = value;
                    LoadHikeForEdit(value);
                }
            }
        }

        private async void LoadHikeForEdit(int hikeId)
        {
            try
            {
                var hike = await _databaseService.GetHikeByIdAsync(hikeId);
                if (hike != null)
                {
                    Title = "Edit Hike";
                    Name = hike.Name;
                    Location = hike.Location;
                    DateOfHike = hike.DateOfHike; // Sẽ tự động gán
                    ParkingAvailable = hike.ParkingAvailable;
                    LengthOfHike = hike.LengthOfHike;
                    DifficultyLevel = hike.DifficultyLevel;
                    Description = hike.Description;
                    HikerCount = hike.HikerCount;
                    Equipment = hike.Equipment;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load Hike for edit: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task SaveHike()
        {
            // THAY ĐỔI: Bỏ kiểm tra DateOfHike (vì nó luôn có giá trị)
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Location) ||
                string.IsNullOrWhiteSpace(LengthOfHike))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            var hike = new Hike
            {
                Id = _currentHikeId,
                Name = Name,
                Location = Location,
                DateOfHike = DateOfHike, // Gán DateTime
                ParkingAvailable = ParkingAvailable,
                LengthOfHike = LengthOfHike,
                DifficultyLevel = DifficultyLevel,
                Description = Description,
                HikerCount = HikerCount,
                Equipment = Equipment
            };

            if (_currentHikeId == 0)
            {
                await _databaseService.AddHikeAsync(hike);
            }
            else
            {
                await _databaseService.UpdateHikeAsync(hike);
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}