using SQLite;

namespace MHiker_CrossPlatform.Models
{
    [Table("hikes")]
    public class Hike
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateOfHike { get; set; } = DateTime.Now; // THAY ĐỔI: Trở lại DateTime
        public bool ParkingAvailable { get; set; }
        public string LengthOfHike { get; set; }
        public string DifficultyLevel { get; set; }
        public string Description { get; set; }
        public string HikerCount { get; set; }
        public string Equipment { get; set; }
    }
}