using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondBrain.Common
{
    public class Note
    {
        public int Idnote { get; set; }
        public int Iddossier { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string ContenuMarkdown { get; set; }
        public DateTime? DateCreation { get; set; }
        public string UserId { get; set; }
    }
}
