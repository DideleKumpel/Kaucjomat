using Kaucjomat.ViewModel;

namespace Kaucjomat.View;

public partial class VouchersArchivesView : ContentPage
{
	public VouchersArchivesView(VouchersArchivesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Sprawdzamy, czy BindingContext to nasz ViewModel
        if (BindingContext is VouchersArchivesViewModel vm)
        {
            vm.RefreshData();
        }
    }
}