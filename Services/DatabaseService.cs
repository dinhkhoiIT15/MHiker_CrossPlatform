using MHiker_CrossPlatform.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHiker_CrossPlatform.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        private async Task Init()
        {
            if (_database != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MHiker.db");
            _database = new SQLiteAsyncConnection(databasePath);
            await _database.CreateTableAsync<Hike>();
        }

        public async Task<List<Hike>> GetHikesAsync()
        {
            await Init();
            return await _database.Table<Hike>().ToListAsync();
        }

        // --- VỊ TRÍ SỬA ---: Thêm hàm lấy Hike bằng ID
        public async Task<Hike> GetHikeAsync(int id)
        {
            await Init();
            return await _database.Table<Hike>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddHikeAsync(Hike hike)
        {
            await Init();
            return await _database.InsertAsync(hike);
        }

        // --- VỊ TRÍ SỬA ---: Thêm hàm cập nhật Hike
        public async Task<int> UpdateHikeAsync(Hike hike)
        {
            await Init();
            return await _database.UpdateAsync(hike);
        }

        public async Task<int> DeleteHikeAsync(Hike hike)
        {
            await Init();
            return await _database.DeleteAsync(hike);
        }
    }
}
