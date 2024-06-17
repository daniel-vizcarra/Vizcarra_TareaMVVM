namespace AppApuntes_DanielVizcarra
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.NotePageDV), typeof(Views.NotePageDV));

        }
    }
}
