namespace AppApuntes_DanielVizcarra.Views;

public partial class AboutPageDV : ContentPage
{
	public AboutPageDV()
	{
		InitializeComponent();
	}

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.AboutDV about)
        {
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
        }
    }
}