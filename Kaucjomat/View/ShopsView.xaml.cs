using Kaucjomat.ViewModel;

namespace Kaucjomat.View;

public partial class ShopsView : ContentPage
{
	public ShopsView(ShopsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}