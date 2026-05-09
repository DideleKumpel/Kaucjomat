using Kaucjomat.ViewModel;

namespace Kaucjomat.View;

public partial class AddNewView : ContentPage
{
	public AddNewView(AddNewViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Sprawdzamy, czy BindingContext to nasz ViewModel
        if (BindingContext is AddNewViewModel vm)
        {
            vm.LoadStoresCommand.Execute(null);
        }
    }
}