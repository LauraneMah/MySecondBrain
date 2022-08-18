using MySecondBrain.Infrastructure.DB;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecondBrain.Infrastructure.ElasticSearch.IndexDocuments
{
    [ElasticsearchType(IdProperty = nameof(NoteDocument.NoteId))]
    public class NoteDocument
    {
        [Keyword(Name = nameof(NoteId))]
        public int NoteId { get; set; }

        [Keyword(Name = nameof(DossierId))]
        public int DossierId { get; set; }

        [Text(Name = nameof(NoteTitre))]
        public string NoteTitre { get; set; }

        [Text(Name = nameof(NoteDescription))]
        public string NoteDescription { get; set; }

        [Text(Name = nameof(ContenuNote))]
        public string ContenuNote { get; set; }

        [Date(Name = nameof(DateCreationNote))]
        public DateTime DateCreationNote { get; set; }

        [Keyword(Name = nameof(IdUser))]
        public string IdUser { get; set; }


    }
}