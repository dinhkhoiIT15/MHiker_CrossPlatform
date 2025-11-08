using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string DateOfHike { get; set; } // THAY ĐỔI: Từ DateTime sang string
        public bool ParkingAvailable { get; set; } // THAY ĐỔI: Tên (bỏ 'Is')
        public string LengthOfHike { get; set; } // THAY ĐỔI: Từ double sang string
        public string DifficultyLevel { get; set; }
        public string Description { get; set; }
        public string HikerCount { get; set; } // THAY ĐỔI: Từ int sang string
        public string Equipment { get; set; } // THÊM MỚI
        // ĐÃ XÓA: EstimatedTime
    }
}