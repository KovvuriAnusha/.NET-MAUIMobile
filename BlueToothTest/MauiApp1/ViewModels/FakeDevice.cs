using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    public class FakeDevice : IDevice
    {
        public FakeDevice(string name, Guid id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; }
        public Guid Id { get; }

        public int Rssi => -60;
        public object NativeDevice => null;

        public DeviceState State => DeviceState.Disconnected;

        public IReadOnlyList<AdvertisementRecord> AdvertisementRecords => new List<AdvertisementRecord>();

        public bool IsConnectable => true;
        public bool SupportsIsConnectable => true;
        public DeviceBondState BondState => DeviceBondState.NotBonded;

        public void Dispose()
        {
            // Nothing to clean up
        }

        public Task<IService> GetServiceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IService>(null);
        }

        public Task<IReadOnlyList<IService>> GetServicesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IReadOnlyList<IService>>(new List<IService>());
        }

        public Task<int> RequestMtuAsync(int requestValue)
        {
            return Task.FromResult(requestValue);
        }

        public bool UpdateConnectionInterval(ConnectionInterval interval)
        {
            return true;
        }

        public bool UpdateConnectionParameters(ConnectParameters connectParameters = default)
        {
            return true;
        }

        public Task<bool> UpdateRssiAsync()
        {
            return Task.FromResult(true);
        }
    }
}
