using Kaucjomat.ViewModel;

namespace Kaucjomat.View;

public partial class AddNewView : ContentPage
{
	public AddNewView(AddNewViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}