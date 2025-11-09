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
    public partial class AddHikeViewModel : ObservableObject, IQueryAttributable
    {
        private readonly DatabaseService _databaseService;

        public AddHikeViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [ObservableProperty]
        private int _hikeId;

        [ObservableProperty]
        private string _pageTitle = "Add New Hike";

        [ObservableProperty]
        private bool _isEditMode = false;

        [ObservableProperty]
        private string _saveButtonText = "Save Hike";


        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _location;

        [ObservableProperty]
        private DateTime _dateOfHike = DateTime.Now;

        [ObservableProperty]
        private string _parkingAvailable = "No";

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


        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("HikeId", out var id))
            {
                HikeId = Convert.ToInt32(id);
                if (HikeId > 0)
                {
                    PageTitle = "Edit / Details Hike";
                    IsEditMode = true;
                    SaveButtonText = "Update Hike";
                    await LoadHikeAsync();
                }
                else
                {
                    PageTitle = "Add New Hike";
                    IsEditMode = false;
                    SaveButtonText = "Save Hike";
                    Name = string.Empty;
                    Location = string.Empty;
                    DateOfHike = DateTime.Now;
                    ParkingAvailable = "No";
                    LengthOfHike = string.Empty;
                    DifficultyLevel = null;
                    Description = string.Empty;
                    HikerCount = string.Empty;
                    Equipment = string.Empty;
                }
            }
        }

        private async Task LoadHikeAsync()
        {
            var hike = await _databaseService.GetHikeByIdAsync(HikeId);
            if (hike != null)
            {
                Name = hike.Name;
                Location = hike.Location;
                DateOfHike = hike.DateOfHike;
                ParkingAvailable = hike.ParkingAvailable;
                LengthOfHike = hike.LengthOfHike;
                DifficultyLevel = hike.DifficultyLevel;
                Description = hike.Description;
                HikerCount = hike.HikerCount;
                Equipment = hike.Equipment;
            }
        }

        [RelayCommand]
        private async Task SaveHike()
        {
            if (HikeId == 0 && (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Location) ||
                string.IsNullOrWhiteSpace(LengthOfHike) ||
                string.IsNullOrWhiteSpace(ParkingAvailable) ||
                string.IsNullOrWhiteSpace(DifficultyLevel)))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all required fields (Name, Location, Length, Parking Available, Difficulty Level)", "OK");
                return;
            }

            var hike = new Hike
            {
                Id = HikeId,
                Name = Name,
                Location = Location,
                DateOfHike = DateOfHike,
                ParkingAvailable = ParkingAvailable,
                LengthOfHike = LengthOfHike,
                DifficultyLevel = DifficultyLevel,
                Description = Description,
                HikerCount = HikerCount,
                Equipment = Equipment
            };

            if (HikeId > 0)
            {
                await _databaseService.UpdateHikeAsync(hike);
            }
            else
            {
                await _databaseService.AddHikeAsync(hike);
            }

            await Shell.Current.GoToAsync(".."); 
        }

        [RelayCommand]
        private async Task DeleteHike()
        {
            if (HikeId == 0) return;

            bool confirm = await Shell.Current.DisplayAlert("Delete Hike", $"Are you sure you want to delete '{Name}'?", "Yes", "No");
            if (confirm)
            {
                var hike = await _databaseService.GetHikeByIdAsync(HikeId);
                if (hike != null)
                {
                    await _databaseService.DeleteHikeAsync(hike);
                }
                await Shell.Current.GoToAsync(".."); 
            }
        }
    }
}