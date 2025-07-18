using MauiApp1.Models;
using MauiApp1.Services.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection _db;

        public DatabaseService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "devices.db3");
            _db = new SQLiteAsyncConnection(dbPath);
        }
        public async Task InitializeAsync()
        {
            await _db.CreateTableAsync<BleDeviceModel>();
        }

        public Task SaveDeviceAsync(BleDeviceModel device)
        {
            return _db.InsertOrReplaceAsync(device);
        }

        public Task<List<BleDeviceModel>> GetSavedDevicesAsync()
        {
            return _db.Table<BleDeviceModel>().ToListAsync();
        }

        public Task DeleteDeviceAsync(Guid id)
        {
            return _db.DeleteAsync<BleDeviceModel>(id);
        }

        public Task ClearAllAsync()
        {
            return _db.DeleteAllAsync<BleDeviceModel>();
        }
    }
}
