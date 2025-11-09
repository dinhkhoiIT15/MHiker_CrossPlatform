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
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "M_Hiker.db");
        }

        private async Task Init()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(_databasePath);
            await _database.CreateTableAsync<Hike>();
        }

        public async Task<Hike> GetHikeByIdAsync(int id)
        {
            await Init();
            return await _database.Table<Hike>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Hike>> GetHikesAsync()
        {
            await Init();
            return await _database.Table<Hike>().ToListAsync();
        }

        public async Task<int> AddHikeAsync(Hike hike)
        {
            await Init();
            return await _database.InsertAsync(hike);
        }

        public async Task<int> DeleteHikeAsync(Hike hike)
        {
            await Init();
            return await _database.DeleteAsync(hike);
        }

        public async Task<int> UpdateHikeAsync(Hike hike)
        {
            await Init();
            return await _database.UpdateAsync(hike);
        }
    }
}