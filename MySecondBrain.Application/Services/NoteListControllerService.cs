using MySecondBrain.Application.ViewModels;
using MySecondBrain.Domain.Services;

using MySecondBrain.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondBrain.Application.Services
{
    public class NoteListControllerService
    {
        public NoteListViewModel GetNotesListViewModel()
        {
            NoteService NoteService = new NoteService();

            var NotesList = NoteService.getNotes();

            var ViewModel = new NoteListViewModel();

            ViewModel.notes = NotesList;

            return ViewModel;
        }

        
    }
}
