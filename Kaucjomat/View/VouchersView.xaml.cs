using Kaucjomat.ViewModel;

namespace Kaucjomat.View;

public partial class VouchersView : ContentPage
{
	public VouchersView(VouchersViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Sprawdzamy, czy BindingContext to nasz ViewModel
        if (BindingContext is VouchersViewModel vm)
        {
            vm.RefreshData();
        }
    }
}