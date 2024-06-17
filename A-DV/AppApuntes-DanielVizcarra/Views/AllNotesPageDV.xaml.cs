namespace AppApuntes_DanielVizcarra.Views;

public partial class AllNotesPageDV : ContentPage
{
    public AllNotesPageDV()
    {
        InitializeComponent();

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }

}