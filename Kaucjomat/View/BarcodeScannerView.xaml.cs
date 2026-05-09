using CommunityToolkit.Mvvm.Messaging;
using Kaucjomat.Messages;
using Kaucjomat.ViewModel;
using ZXing.Net.Maui;

namespace Kaucjomat.View;

public partial class BarcodeScannerView : ContentPage
{
    private bool _isProcessing = false;

    public BarcodeScannerView()
    {
        InitializeComponent();

        barcodeReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = false
        };
    }

    private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (_isProcessing) return;

        var firstCode = e.Results.FirstOrDefault();
        if (firstCode == null) return;

        _isProcessing = true;
        barcodeReader.IsDetecting = false;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            WeakReferenceMessenger.Default.Send(new BarcodeScannedMessage(firstCode.Value));
            await Navigation.PopAsync();
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _isProcessing = false;
        barcodeReader.IsDetecting = true;
    }
}