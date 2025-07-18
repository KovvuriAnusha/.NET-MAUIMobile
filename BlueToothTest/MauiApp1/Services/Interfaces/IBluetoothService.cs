using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp1.Services.Interfaces
{
    public interface IBluetoothService
    {
        Task<IEnumerable<IDevice>> ScanForDevicesAsync();
    }
}
