using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Services.Interfaces;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IBluetoothService _bluetoothService;
        private readonly IDatabaseService _databaseService;
        // Observable collection of devices (for UI binding)
        public ObservableCollection<IDevice> Devices { get; }

        public ObservableCollection<BleDeviceModel> SavedDevices { get; }
        // Status message to show scan/connect status
        [ObservableProperty]
        private string status = "Idle";
        public MainViewModel(IBluetoothService bluetoothService, IDatabaseService databaseService)
        {
            _bluetoothService = bluetoothService;
            _databaseService = databaseService;
            Devices = new ObservableCollection<IDevice>();
            SavedDevices = new ObservableCollection<BleDeviceModel>();
        }


        // Command to simulate BLE scan
        [RelayCommand]
        public async Task ScanAsync()
        {
            Status = "Scanning for BLE devices...";
            Devices.Clear();

            var scannedDevices = await _bluetoothService.ScanForDevicesAsync();
            foreach (var device in scannedDevices.Where(d => !string.IsNullOrEmpty(d.Name)))
            {
                Devices.Add(device);
            }

            Status = $"Scan complete. Found {Devices.Count} device(s).";
        }

        [RelayCommand]
        public async Task SaveDeviceAsync(IDevice device)
        {
            if (device == null)
                return;

            var model = new BleDeviceModel
            {
                Id = device.Id,
                Name = device.Name
            };

            await _databaseService.SaveDeviceAsync(model);
            SavedDevices.Add(model);

            Status = $"Saved device: {device.Name}";
        }

        [RelayCommand]
        public async Task LoadSavedDevicesAsync()
        {
            var list = await _databaseService.GetSavedDevicesAsync();
            SavedDevices.Clear();

            foreach (var device in list)
            {
                SavedDevices.Add(device);
            }

            Status = $"Loaded {SavedDevices.Count} saved device(s).";
        }

        //// Command to simulate connect to selected device
        //[RelayCommand]
        //public async Task ConnectAsync(IDevice device)
        //{
        //    Status = $"Connecting to {device.Name}...";
        //    await Task.Delay(1000); // Simulate connection
        //    Status = $"Connected to {device.Name}.";
        //}

        //// Command to simulate connect to selected device
        //[RelayCommand]
        //public async Task ConnectAndReadAsync(IDevice selectedDevice)
        //{
        //    Status = $"Connecting to {selectedDevice.Name}...";
        //    await _bleService.ConnectToDeviceAsync(selectedDevice);

        //    // Replace with actual characteristic UUIDs from your BLE device
        //    var serviceId = Guid.Parse("0000180D-0000-1000-8000-00805f9b34fb");
        //    var charId = Guid.Parse("00002A37-0000-1000-8000-00805f9b34fb");

        //    var data = await _bleService.ReadDataAsync(selectedDevice, serviceId, charId);
        //    var message = BitConverter.ToString(data);

        //    await _dbService.SaveDataAsync(new BleDataModel
        //    {
        //        DeviceName = selectedDevice.Name,
        //        DataReceived = message,
        //        Timestamp = DateTime.Now
        //    });

        //    Status = $"Data: {message}";
        //}
    }
}
