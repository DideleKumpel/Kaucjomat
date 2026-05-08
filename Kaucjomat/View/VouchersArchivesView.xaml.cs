using Kaucjomat.ViewModel;

namespace Kaucjomat.View;

public partial class VouchersArchivesView : ContentPage
{
	public VouchersArchivesView(VouchersArchivesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}