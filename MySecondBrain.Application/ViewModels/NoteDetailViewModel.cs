using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondBrain.Application.ViewModels
{
    public class NoteDetailViewModel
    {
        public Infrastructure.DB.Note Note { get; set; }
        public IEnumerable<Infrastructure.DB.Dossier> Dossiers { get; set; }
    }
}
