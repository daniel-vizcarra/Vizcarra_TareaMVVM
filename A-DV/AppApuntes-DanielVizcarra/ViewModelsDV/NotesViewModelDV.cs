using CommunityToolkit.Mvvm.Input;
using AppApuntes_DanielVizcarra.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace AppApuntes_DanielVizcarra.ViewModelsDV
{
    internal class NotesViewModelDV : IQueryAttributable
    {
        public ObservableCollection<ViewModelsDV.NoteViewModelDV> AllNotes { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public NotesViewModelDV()
        {
            AllNotes = new ObservableCollection<ViewModelsDV.NoteViewModelDV>(Models.NoteDV.LoadAll().Select(n => new NoteViewModelDV(n)));
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<ViewModelsDV.NoteViewModelDV>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.NotePageDV));
        }

        private async Task SelectNoteAsync(ViewModelsDV.NoteViewModelDV note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.NotePageDV)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                NoteViewModelDV matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                NoteViewModelDV matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                }

                // If note isn't found, it's new; add it.
                else
                    AllNotes.Insert(0, new NoteViewModelDV(Models.NoteDV.Load(noteId)));
            }
        }
    }
}
