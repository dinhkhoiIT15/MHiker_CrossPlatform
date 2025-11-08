using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MHiker_CrossPlatform.Models;
using MHiker_CrossPlatform.Services;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MHiker_CrossPlatform.ViewModels
{
    // --- VỊ TRÍ SỬA ---: Thêm QueryProperty để nhận ID từ trang danh sách
    [QueryProperty(nameof(HikeId), "HikeId")]
    public partial class AddHikeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public AddHikeViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;

            DifficultyLevels = new ObservableCollection<string>
            {
                "Easy", "Medium", "Hard", "Very Hard"
            };
        }

        // --- VỊ TRÍ SỬA ---: Property để lưu hike đang được chỉnh sửa
        [ObservableProperty]
        private Hike hikeToEdit;

        [ObservableProperty]
        private int hikeId;

        // --- VỊ TRÍ SỬA ---: Hàm sẽ tự động được gọi khi HikeId được truyền vào
        async partial void OnHikeIdChanged(int value)
        {
            if (value != 0)
            {
                // Chế độ Edit: Tải hike từ CSDL
                HikeToEdit = await _databaseService.GetHikeAsync(value);
                Name = HikeToEdit.Name;
                Location = HikeToEdit.Location;
                DateOfHike = HikeToEdit.DateOfHike;
                IsParkingAvailable = HikeToEdit.IsParkingAvailable;
                LengthOfHike = HikeToEdit.LengthOfHike;
                SelectedDifficulty = HikeToEdit.DifficultyLevel;
                Description = HikeToEdit.Description;
                HikerCount = HikeToEdit.HikerCount;
                EstimatedTime = HikeToEdit.EstimatedTime;
            }
            else
            {
                // Chế độ Add: Reset các trường
                Name = string.Empty;
                Location = string.Empty;
                DateOfHike = DateTime.Now;
                IsParkingAvailable = false;
                LengthOfHike = 0;
                SelectedDifficulty = DifficultyLevels[0];
                Description = string.Empty;
                HikerCount = 0;
                EstimatedTime = 0;
            }
        }


        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string location;
        [ObservableProperty]
        private DateTime dateOfHike = DateTime.Now;
        [ObservableProperty]
        private bool isParkingAvailable;
        [ObservableProperty]
        private double lengthOfHike;
        [ObservableProperty]
        private string selectedDifficulty;
        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private int hikerCount;
        [ObservableProperty]
        private double estimatedTime;

        public ObservableCollection<string> DifficultyLevels { get; }

        [RelayCommand]
        private async Task SaveHike()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Location) || LengthOfHike <= 0)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please fill all required fields (Name, Location, Length).", "OK");
                return;
            }

            // --- VỊ TRÍ SỬA ---: Cập nhật logic để xử lý cả Add và Edit
            bool isNewHike = (HikeToEdit == null || HikeToEdit.Id == 0);

            Hike hikeToSave = isNewHike ? new Hike() : HikeToEdit;

            hikeToSave.Name = this.Name;
            hikeToSave.Location = this.Location;
            hikeToSave.DateOfHike = this.DateOfHike;
            hikeToSave.IsParkingAvailable = this.IsParkingAvailable;
            hikeToSave.LengthOfHike = this.LengthOfHike;
            hikeToSave.DifficultyLevel = this.SelectedDifficulty;
            hikeToSave.Description = this.Description;
            hikeToSave.HikerCount = this.HikerCount;
            hikeToSave.EstimatedTime = this.EstimatedTime;


            var confirmationMessage = new StringBuilder();
            confirmationMessage.AppendLine("Please confirm the details below:\n");
            confirmationMessage.AppendLine($"Name: {hikeToSave.Name}");
            // ... (Thêm các dòng khác nếu muốn)

            string actionButton = isNewHike ? "Confirm & Save" : "Confirm & Update";
            bool isConfirmed = await Shell.Current.DisplayAlert(
                "Confirm Hike Details",
                confirmationMessage.ToString(),
                actionButton, "Edit");

            if (!isConfirmed) return;

            if (isNewHike)
            {
                await _databaseService.AddHikeAsync(hikeToSave);
                await Shell.Current.DisplayAlert("Success", "Hike details saved to database.", "OK");
            }
            else
            {
                await _databaseService.UpdateHikeAsync(hikeToSave);
                await Shell.Current.DisplayAlert("Success", "Hike details updated successfully.", "OK");
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}

