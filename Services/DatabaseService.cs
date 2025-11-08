using MHiker_CrossPlatform.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHiker_CrossPlatform.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;
        private readonly string _databasePath;

        public DatabaseService()
        {
            // Xác định đường dẫn CSDL
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "M_Hiker.db");
        }

        // Khởi tạo CSDL và Bảng
        private async Task Init()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(_databasePath);
            await _database.CreateTableAsync<Hike>();
        }

        // Lấy tất cả
        public async Task<List<Hike>> GetHikesAsync()
        {
            await Init();
            return await _database.Table<Hike>().ToListAsync();
        }

        // THÊM MỚI (Phương thức bị thiếu)
        // Lấy Hike theo ID
        public async Task<Hike> GetHikeByIdAsync(int id)
        {
            await Init();
            return await _database.Table<Hike>().FirstOrDefaultAsync(h => h.Id == id);
        }
        // KẾT THÚC THÊM MỚI

        // THÊM
        public async Task<int> AddHikeAsync(Hike hike)
        {
            await Init();
            return await _database.InsertAsync(hike);
        }

        // XÓA
        public async Task<int> DeleteHikeAsync(Hike hike)
        {
            await Init();
            return await _database.DeleteAsync(hike);
        }

        // CẬP NHẬT
        public async Task<int> UpdateHikeAsync(Hike hike)
        {
            await Init();
            return await _database.UpdateAsync(hike);
        }
    }
}