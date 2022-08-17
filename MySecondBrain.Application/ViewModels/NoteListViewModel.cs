using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondBrain.Application.ViewModels
{
    public class NoteListViewModel
    {
        public List<Common.Note> notes { get; set; }

        public List<Infrastructure.DB.Note> Notes { get; set; }





    }
}
