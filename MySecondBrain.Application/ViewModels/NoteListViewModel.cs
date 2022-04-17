using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondBrain.Application.ViewModels
{
    public class NoteListViewModel
    {
        public IEnumerable<Infrastructure.DB.Note> Notes { get; set; }

        public int NotesCount
        {
            get
            {
                return Notes.Count();
            }
        }
    }
}
