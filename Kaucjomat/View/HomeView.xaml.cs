using Kaucjomat.ViewModel;

namespace Kaucjomat.View;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}