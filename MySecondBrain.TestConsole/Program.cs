using System;
using System.Collections.Generic;
using System.Linq;

namespace MySecondBrain.TestConsole
{
    class Program
    {
        /// <summary>
        /// Méthode principale qui appelle TestServices.
        /// </summary>
        /// <returns></returns>
        static void Main(string[] args)
        {

            if (Domain.Services.ElasticSearch.ElasticSearchServiceAgent.CreateIndexes())
            {
                Console.WriteLine("Index créés avec succès :-)");

                IndexDatabaseNote();
                IndexDatabaseTag();

                var allNotes = Domain.Services.ElasticSearch.ElasticSearchServiceAgent.GetAllNotes();
                var notesFound = Domain.Services.ElasticSearch.ElasticSearchServiceAgent.SearchNotesByTitle("Note");
            }
            else
                Console.WriteLine("Problème pendant la création des index!");

        }

        private static void IndexDatabaseNote()
        {
            var noteDocuments = new List<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument>();

            using (Infrastructure.DB.MySecondBrain_LMContext db = new Infrastructure.DB.MySecondBrain_LMContext())
            {
                // on construit la liste des documents à indexer sur base du contenu de la DB
                foreach (var note in db.Notes.ToList())
                {
                    var noteDocument = new Infrastructure.ElasticSearch.IndexDocuments.NoteDocument()
                    {
                        NoteId = note.Idnote,
                        NoteTitre = note.Titre,
                        NoteDescription = note.Description,
                        ContenuNote = note.Contenu,
                        IdUser = note.UserId,
                        DossierId = note.Iddossier,
                        DateCreationNote = DateTime.Now,

                    };

                    noteDocuments.Add(noteDocument);
                }
            }

            // on indexe
            if (Domain.Services.ElasticSearch.ElasticSearchServiceAgent.IndexAllNotes(noteDocuments))
                Console.WriteLine("Notes indexées avec succès :-)");
            else
                Console.WriteLine("Une erreur s'est produite pendant l'indexation des notes!");
        }

        public static void IndexDatabaseTag()
        {
            var tagDocuments = new List<Infrastructure.ElasticSearch.IndexDocuments.TagDocument>();

            using (Infrastructure.DB.MySecondBrain_LMContext db = new Infrastructure.DB.MySecondBrain_LMContext())
            {
                // on construit la liste des documents à indexer sur base du contenu de la DB
                foreach (var tag in db.Tags.ToList())
                {
                    var tagDocument = new Infrastructure.ElasticSearch.IndexDocuments.TagDocument()
                    {
                        TagId = tag.Idtag,
                        TagName = tag.Nom,
                        TagUserId = tag.UserId,
                    };

                    tagDocuments.Add(tagDocument);
                }
            }

            // on indexe
            if (Domain.Services.ElasticSearch.ElasticSearchServiceAgent.IndexAllTags(tagDocuments))
                Console.WriteLine("Tags indexés avec succès :-)");
            else
                Console.WriteLine("Une erreur s'est produite pendant l'indexation des tags!");
        }
    }
}
