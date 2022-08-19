using MySecondBrain.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MySecondBrain.Domain.Services
{
    public class NoteService
    {
        public List<Note> GetNotes()
        {
            MySecondBrain_LMContext mySecondBrainContext = new MySecondBrain_LMContext();

            List<Note> notes1 = new List<Note>();

            var notes = mySecondBrainContext.Notes.ToList();

            foreach (var getNote in notes)
            {
                Note note = new Note();

                note.Idnote = getNote.Idnote;
                note.Titre = getNote.Titre;
                note.Description = getNote.Description;
                note.Contenu = getNote.Contenu;
                note.DateCreation = getNote.DateCreation;
                note.Iddossier = getNote.Iddossier;
                note.UserId = getNote.UserId;
                notes1.Add(note);
            }

            return notes1;
        }
        public static Note GetNote(int noteId)
        {
            using (MySecondBrain_LMContext db = new MySecondBrain_LMContext())
            {
                return db.Notes.Find(noteId);
            }
        }

        public static List<Note> GetAllNotesOfUser(string userId)
        {
            using (MySecondBrain_LMContext db = new MySecondBrain_LMContext())
            {
                return db.Notes.Where(n => n.User.Id == userId).ToList();
            }
        }

        public static void CreateNote(Note note, string userId )
        {
            //if (userId == null) 
            //{ 
                   //redirect to Login
            //}
            note.UserId = userId;
            note.DateCreation = DateTime.Now;
            note.Iddossier = 3; // A MODIFIER pour qu'on puisse choisir un dossier nous mêmes
            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            db.Notes.Add(note);
            db.SaveChanges();
        }

        public static void EditNote(Note note)
        {
            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            Note noteToUpdate = db.Notes.Find(note.Idnote);
            {
                if (noteToUpdate != null)
                {
                    noteToUpdate.Titre = note.Titre;
                    noteToUpdate.Contenu = note.Contenu;
                    noteToUpdate.NoteTags = note.NoteTags;
                    noteToUpdate.Description = note.Description;
                    db.Update(noteToUpdate);
                }
            }
            db.SaveChanges();
        }


        public static void DeleteNote(int noteId)
        {
            MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            var noteToRemove = db.Notes.SingleOrDefault(a => a.Idnote == noteId);
            if (noteToRemove != null)
            {
                db.Notes.Remove(noteToRemove);
                db.SaveChanges();
            }

        }

    }
}
