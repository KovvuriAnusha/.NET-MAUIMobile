using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Services.Interfaces;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;


namespace MauiApp1.Services
{
    public class BluetoothService : IBluetoothService
    {
        private readonly IBluetoothLE _bluetoothLE;
        private readonly IAdapter _adapter;

        public BluetoothService()
        {
            _bluetoothLE = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;
        }

        public async Task<IEnumerable<IDevice>> ScanForDevicesAsync()
        {
            if (!_bluetoothLE.IsOn)
                return Enumerable.Empty<IDevice>();

            var devices = new List<IDevice>();

            _adapter.DeviceDiscovered += (s, a) =>
            {
                if (!devices.Any(d => d.Id == a.Device.Id))
                    devices.Add(a.Device);
            };

            await _adapter.StartScanningForDevicesAsync();

            return devices;
        }
        public async Task<List<IDevice>> ScanDevicesAsync()
        {
            var devices = new List<IDevice>();
            _adapter.DeviceDiscovered += (s, a) =>
            {
                if (!devices.Any(d => d.Id == a.Device.Id))
                    devices.Add(a.Device);
            };

            await _adapter.StartScanningForDevicesAsync();
            return devices;
        }
        public async Task<IDevice> ConnectToDeviceAsync(IDevice device)
        {
            await _adapter.ConnectToDeviceAsync(device);
            return device;
        }

        public async Task<byte[]> ReadDataAsync(IDevice device, Guid serviceId, Guid charId)
        {
            var service = await device.GetServiceAsync(serviceId);
            var characteristic = await service.GetCharacteristicAsync(charId);
            var bytes = await characteristic.ReadAsync();
            return bytes.data;
        }
    }
}
