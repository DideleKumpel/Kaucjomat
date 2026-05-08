using CommunityToolkit.Maui.Views;
using Kaucjomat.Model;

namespace Kaucjomat.View.Popup;

public partial class BarcodePopup : CommunityToolkit.Maui.Views.Popup
{
	public BarcodePopup(Voucher voucher)
	{
		InitializeComponent();
        BindingContext = voucher;
    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        CloseAsync();
    }

}