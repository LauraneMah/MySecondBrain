using MySecondBrain.Application.ViewModels;
using MySecondBrain.Domain.Services;
using MySecondBrain.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MySecondBrain.Application.Services
{
    public class NoteControllerService
    {
        public NoteListViewModel GetNotesListViewModel()
        {
            NoteService NoteService = new NoteService();

            var NotesList = NoteService.getNotes();

            var ViewModel = new NoteListViewModel();

            ViewModel.notes = NotesList;

            return ViewModel;
        }

        public static List<Infrastructure.DB.Note> getNotesListOfUser(string userId)
        {
            var notes = NoteService.GetAllNotesOfUser(userId);

            NoteListViewModel vm = new NoteListViewModel()
            {
                Notes = notes,
            };

            return vm.Notes;
        }


        public static void CreateNote(Infrastructure.DB.Note note, string userId)
        {
            Domain.Services.NoteService.CreateNote(note, userId);
        }



        //public NoteListViewModel GetNoteDetails(int noteId)
        //{

        //}


    }

}

