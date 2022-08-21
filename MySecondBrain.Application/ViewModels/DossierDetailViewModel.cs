using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySecondBrain.Infrastructure.DB;

namespace MySecondBrain.Application.ViewModels
{
     public class DossierDetailViewModel
    {
        public Dossier Dossier { get; set; }

        public List<SelectListItem> DossierList { get; set; }

        public int IDDossierParent { get; set; }

    }
}
