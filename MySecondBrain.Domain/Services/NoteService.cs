﻿using MySecondBrain.Common;
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
        public List<Common.Note> getNotes()
        {
            Infrastructure.DB.MySecondBrainContext mySecondBrainContext = new MySecondBrainContext();

            List<Common.Note> notes1 = new List<Common.Note>();

            var notes = mySecondBrainContext.Notes.ToList();
            foreach (var dbnote in notes)
            {
                Common.Note note = new Common.Note();

                note.Idnote = dbnote.Idnote;
                note.Titre = dbnote.Titre;
                // etc ...
                notes1.Add(note);
            }

            return notes1;
        }

        public static List<Infrastructure.DB.Note> GetAllNotesOfUser(string userId)
        {
            using (MySecondBrainContext db = new MySecondBrainContext())
            {
                return db.Notes.Where(n => n.User.Id == userId).ToList();
            }
        }

        public static void CreateNote(Infrastructure.DB.Note note, string userId )
        {
            note.UserId = userId;
            Infrastructure.DB.MySecondBrainContext db = new MySecondBrainContext();
            db.Notes.Add(note);
            db.SaveChanges();
        }

        public static void EditNote(Infrastructure.DB.Note note)
        {
            Infrastructure.DB.MySecondBrainContext db = new MySecondBrainContext();

            Infrastructure.DB.Note noteToUpdate = db.Notes.Find(note.Idnote);
            {
                if (noteToUpdate != null)
                {
                    noteToUpdate.Titre = note.Titre;
                    noteToUpdate.ContenuMarkdown = note.ContenuMarkdown;
                    noteToUpdate.NoteTags = note.NoteTags;
                    db.Update(noteToUpdate);
                }
            }
            db.SaveChanges();
        }


        public static void DeleteNote(int noteId)
        {
            Infrastructure.DB.MySecondBrainContext db = new MySecondBrainContext();

            var noteToRemove = db.Notes.SingleOrDefault(a => a.Idnote == noteId);
            if (noteToRemove != null)
            {
                db.Notes.Remove(noteToRemove);
                db.SaveChanges();
            }

        }

    }
}
