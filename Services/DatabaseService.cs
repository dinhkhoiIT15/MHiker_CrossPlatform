using MHiker_CrossPlatform.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MHiker_CrossPlatform.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        async Task Init()
        {
            if (_database != null) return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "M_Hiker.db");
            _database = new SQLiteAsyncConnection(databasePath);
            await _database.CreateTableAsync<Hike>();
        }

        public async Task<List<Hike>> GetHikesAsync()
        {
            await Init();
            return await _database.Table<Hike>().ToListAsync();
        }

        public async Task<Hike> GetHikeAsync(int id)
        {
            await Init();
            return await _database.Table<Hike>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveHikeAsync(Hike hike)
        {
            await Init();
            if (hike.Id != 0)
                return await _database.UpdateAsync(hike); // Update nếu đã có ID
            else
                return await _database.InsertAsync(hike); // Insert nếu ID = 0
        }

        public async Task<int> DeleteHikeAsync(Hike hike)
        {
            await Init();
            return await _database.DeleteAsync(hike);
        }
    }
}