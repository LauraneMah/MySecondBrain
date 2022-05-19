using MySecondBrain.Common;
using MySecondBrain.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
