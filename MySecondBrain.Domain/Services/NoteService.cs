using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MySecondBrain.Domain.Services
{
    public class NoteService
    {
        public static List<Infrastructure.DB.Note> GetAllNotes()
        {
            using (Infrastructure.DB.MySecondBrainContext db = new Infrastructure.DB.MySecondBrainContext())
            {
                return db.Notes.ToList();
            }
        }

        public static Infrastructure.DB.Note GetNote(int noteId)
        {
            using (Infrastructure.DB.MySecondBrainContext db = new Infrastructure.DB.MySecondBrainContext())
            {
                return db.Notes.Find(noteId);
            }
        }

        public static void CreateNote(Infrastructure.DB.Note note)
        {
            using (Infrastructure.DB.MySecondBrainContext db = new Infrastructure.DB.MySecondBrainContext())
            {
                db.Notes.Add(note);
                db.SaveChanges();
            }
        }
    }
}
