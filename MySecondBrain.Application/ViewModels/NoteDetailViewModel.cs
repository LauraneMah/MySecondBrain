using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<SelectListItem> DossierList { get; set; }

        public List<SelectListItem> TagList { get; set; }

        public int IDDossier { get; set; }

        public List<int> IdTagNote { get; set; }
    
    }
}
