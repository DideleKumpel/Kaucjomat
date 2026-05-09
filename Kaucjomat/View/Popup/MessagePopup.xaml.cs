namespace Kaucjomat.View.Popup;

public partial class MessagePopup : CommunityToolkit.Maui.Views.Popup
{
	public MessagePopup(string header, string message)
    {
		InitializeComponent();
        Header.Text = header;
        Message.Text = message;
	}

    private void OnCloseClicked(object sender, EventArgs e)
    {
        CloseAsync();
    }
}