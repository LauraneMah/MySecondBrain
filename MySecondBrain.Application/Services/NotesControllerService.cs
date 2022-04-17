using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondBrain.Application.Services
{
    class NotesControllerService
    {
        public static ViewModels.NoteListViewModel GetNoteList()
        {
            ViewModels.NoteListViewModel vm = new ViewModels.NoteListViewModel();
            vm.Notes = Domain.Services.NoteService.GetAllNotes();

            return vm;
        }

        /// <summary>
        /// Renvoie un ViewModel contenant la note
        /// </summary>
        /// <param name="noteId">Id de la note</param>
        /// <returns>NoteDetailViewModel</returns>
        public static ViewModels.NoteDetailViewModel GetNoteDetails(int noteId)
        {
            Infrastructure.DB.Note note = Domain.Services.NoteService.GetNote(noteId);
            if (note == null)
                return null;

            ViewModels.NoteDetailViewModel vm = new ViewModels.NoteDetailViewModel()
            {
                Note = note
            };

            return vm;

        }

        /// <summary>
        /// Appelle le service de la couche domain pour créer une note
        /// </summary>
        /// <param name="note">Note à créer</param>
        /// <returns></returns>
        public static void CreateNote(Infrastructure.DB.Note note)
        {
            Domain.Services.NoteService.CreateNote(note);
        }
    }
}
