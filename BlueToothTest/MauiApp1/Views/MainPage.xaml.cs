using MauiApp1.Services;
using MauiApp1.ViewModels;
using Plugin.BLE.Abstractions.Contracts;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;


        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
        private async void OnScannedDeviceSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is IDevice selectedDevice)
            {
                await ViewModel.SaveDeviceAsync(selectedDevice);

                // Deselect
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
