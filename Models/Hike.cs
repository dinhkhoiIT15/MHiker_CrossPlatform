using SQLite;
using System;

namespace MHiker_CrossPlatform.Models
{
    [Table("hikes")]
    public class Hike
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateOfHike { get; set; } // Đã đổi sang DateTime
        public bool ParkingAvailable { get; set; }
        public double LengthOfHike { get; set; } // Đã đổi sang double
        public string DifficultyLevel { get; set; }
        public string Description { get; set; }
        public int HikerCount { get; set; } // Đã đổi sang int
        public string Equipment { get; set; }
    }
}