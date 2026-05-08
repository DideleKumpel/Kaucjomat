using Kaucjomat.ViewModel;

namespace Kaucjomat.View;

public partial class VouchersView : ContentPage
{
	public VouchersView(VouchersViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}