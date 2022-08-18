using System;
using System.Collections.Generic;

#nullable disable

namespace MySecondBrain.Infrastructure.DB
{
    public partial class Note
    {
        public Note()
        {
            NoteTags = new HashSet<NoteTag>();
        }

        public int Idnote { get; set; }
        public int Iddossier { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Contenu { get; set; }
        public DateTime? DateCreation { get; set; }
        public string UserId { get; set; }

        public virtual Dossier IddossierNavigation { get; set; }
        public virtual AspNetUser User { get; set; }
        public virtual ICollection<NoteTag> NoteTags { get; set; }
    }
}
