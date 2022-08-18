using MySecondBrain.Common;
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
        public List<Note> getNotes()
        {
            Infrastructure.DB.MySecondBrain_LMContext mySecondBrainContext = new MySecondBrain_LMContext();

            List<Note> notes1 = new List<Note>();

            var notes = mySecondBrainContext.Notes.ToList();
            foreach (var dbnote in notes)
            {
                Note note = new Note();

                note.Idnote = dbnote.Idnote;
                note.Titre = dbnote.Titre;
                // etc ...
                notes1.Add(note);
            }

            return notes1;
        }

        public static List<Infrastructure.DB.Note> GetAllNotesOfUser(string userId)
        {
            using (MySecondBrain_LMContext db = new MySecondBrain_LMContext())
            {
                return db.Notes.Where(n => n.User.Id == userId).ToList();
            }
        }

        public static void CreateNote(Infrastructure.DB.Note note, string userId )
        {
            note.UserId = userId;
            note.Iddossier = 3;
            Infrastructure.DB.MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            db.Notes.Add(note);
            db.SaveChanges();
        }

        public static void EditNote(Infrastructure.DB.Note note)
        {
            Infrastructure.DB.MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            Infrastructure.DB.Note noteToUpdate = db.Notes.Find(note.Idnote);
            {
                if (noteToUpdate != null)
                {
                    noteToUpdate.Titre = note.Titre;
                    noteToUpdate.Contenu = note.Contenu;
                    noteToUpdate.NoteTags = note.NoteTags;
                    db.Update(noteToUpdate);
                }
            }
            db.SaveChanges();
        }


        public static void DeleteNote(int noteId)
        {
            Infrastructure.DB.MySecondBrain_LMContext db = new MySecondBrain_LMContext();

            var noteToRemove = db.Notes.SingleOrDefault(a => a.Idnote == noteId);
            if (noteToRemove != null)
            {
                db.Notes.Remove(noteToRemove);
                db.SaveChanges();
            }

        }

    }
}
