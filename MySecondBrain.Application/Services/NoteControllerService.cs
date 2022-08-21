using MySecondBrain.Application.ViewModels;
using MySecondBrain.Domain.Services;
using MySecondBrain.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MySecondBrain.Application.Services
{
    public class NoteControllerService
    {
        public NoteListViewModel GetNotesListViewModel(string userId)
        {
            NoteService noteService = new NoteService();

            var notesList = noteService.GetNotes();

            var vm = new NoteListViewModel();

            vm.Notes = notesList;

            return vm;
        }

        public static List<Note> GetNotesListOfUser(string userId)
        {
            var notes = NoteService.GetAllNotesOfUser(userId);

            NoteListViewModel vm = new NoteListViewModel()
            {
                Notes = notes,
            };

            return vm.Notes;
        }


        public static void CreateNote(Note note, string userId, int idDossier)
        {
            NoteService.CreateNote(note, userId, idDossier);
        }

        public static void EditNote(Note note)
        {
            NoteService.EditNote(note);
        }


        public static NoteDetailViewModel GetNoteDetail(int noteId)
        {
            Note note = NoteService.GetNote(noteId);

            if(note == null)
            {
                return null;
            }

            NoteDetailViewModel vm = new NoteDetailViewModel()
            {
                Note = note
            };

            return vm;
        }

        public static NoteDetailViewModel GetNoteDetail()
        {

            NoteDetailViewModel vm = new NoteDetailViewModel()
            {
                DossierList = GetDossierList("d504ee08-20f8-45c6-9ae3-b06a30f13ab5")//change for a variable
            };

            return vm;
        }

        public static void DeleteNote(int noteId)
        {
            NoteService.DeleteNote(noteId);
        }

        //GetDossier -> Passe par DossierService

        public static List<SelectListItem> GetDossierList(string userId)
        {
            var dossierList = DossierService.GetAllDossiersOfUser(userId);

            var list = new List<SelectListItem>();

            foreach (var dossier in dossierList)
            {
                list.Add(new SelectListItem
                {
                    Text = dossier.Nom,
                    Value = dossier.Iddossier.ToString()
                });
            }

            return list;
        }

        // TODO Method CreateNoteTag

    }
}


