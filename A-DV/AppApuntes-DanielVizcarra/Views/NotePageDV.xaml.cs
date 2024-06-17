namespace AppApuntes_DanielVizcarra.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePageDV : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    public NotePageDV()
	{
		InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));

        if (File.Exists(_fileName))
            TextEditor.Text = File.ReadAllText(_fileName);
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.NoteDV note)
            File.WriteAllText(note.Filename, TextEditor.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.NoteDV note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void LoadNote(string fileName)
    {
        Models.NoteDV noteModel = new Models.NoteDV();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }

    public string ItemId
    {
        set { LoadNote(value); }
    }

}
