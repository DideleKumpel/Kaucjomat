namespace Kaucjomat.View.Popup;

public partial class ConformationPopUp : CommunityToolkit.Maui.Views.Popup
{
    public bool Result { get; private set; } = false;
    public ConformationPopUp(string header, string message)
	{
		InitializeComponent();
        Header.Text = header;
        Message.Text = message;
    }
    public async void BtnYesClicked(object sender, EventArgs e)
    {
        Result = true;
        await CloseAsync();
    }

    private async void BtnNoClicked(object sender, EventArgs e)
    {
        Result = false;
        await CloseAsync();
    }
}