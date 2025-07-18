using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task InitializeAsync();
        Task SaveDeviceAsync(BleDeviceModel device);
        Task<List<BleDeviceModel>> GetSavedDevicesAsync();
        Task DeleteDeviceAsync(Guid id);
        Task ClearAllAsync();
    }
}
