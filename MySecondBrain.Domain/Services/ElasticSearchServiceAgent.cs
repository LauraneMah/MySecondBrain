using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace MySecondBrain.Domain.Services.ElasticSearch
{
    /// <summary>
    /// Agent de service responsable d'encapsuler les accès à ElasticSearch
    /// </summary>
    public class ElasticSearchServiceAgent
    {
        static string elasticAddress = "http://localhost:9200";
        const string noteIndexName = "notes_index";
        const string tagIndexName = "tag_index";

        /// <summary>
        /// Crée l'objet settings nécessaire pour se connecter à ES
        /// </summary>
        /// <returns></returns>
        static ConnectionSettings GetESConnectionSettings()
        {
            var node = new Uri(elasticAddress);
            var settings = new ConnectionSettings(node);
            settings.ThrowExceptions(alwaysThrow: true);
            settings.PrettyJson(); // Good for DEBUG

            return settings;
        }

        /// <summary>
        ///  Création des index dans ElasticSearch
        /// </summary>
        public static bool CreateIndexes()
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            if (esClient.Indices.Exists(noteIndexName).Exists)
            {
                var response = esClient.Indices.Delete(noteIndexName);

            }

            if (esClient.Indices.Exists(tagIndexName).Exists)
            {
                var response = esClient.Indices.Delete(tagIndexName);
            }

            var createIndexResponse = esClient.Indices.Create(noteIndexName, index => index.Map<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument>(x => x.AutoMap()));
            var createIndexResponseTag = esClient.Indices.Create(tagIndexName, index => index.Map<Infrastructure.ElasticSearch.IndexDocuments.TagDocument>(x => x.AutoMap()));

            return createIndexResponse.IsValid && createIndexResponseTag.IsValid;
        }


        //lors de groschangement dans les tags/notes, réindexer toutes les notes/tags
        public static bool IndexAllNotes(List<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument> noteDocuments)
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            foreach (var noteDocument in noteDocuments)
            {
                var indexResponse = esClient.Index(noteDocument, c => c.Index(noteIndexName));

                if (!indexResponse.IsValid)
                    return false;
            }

            return true;
        }

        public static bool IndexAllTags(List<Infrastructure.ElasticSearch.IndexDocuments.TagDocument> tagDocuments)
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            foreach (var tagDocument in tagDocuments)
            {
                var indexResponse = esClient.Index(tagDocument, c => c.Index(tagIndexName));

                if (!indexResponse.IsValid)
                    return false;
            }

            return true;
        }

        //lors de création d'une note, indexer qu'une seule note
        public static bool IndexNote(Infrastructure.ElasticSearch.IndexDocuments.NoteDocument noteDocument)
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            var indexResponse = esClient.Index(noteDocument, c => c.Index(noteIndexName));

            return indexResponse.IsValid;
        }

        //lors de création d'un tag, indexer qu'un seul tag
        public static bool IndexTag(Infrastructure.ElasticSearch.IndexDocuments.TagDocument tagDocument)
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            var indexResponse = esClient.Index(tagDocument, c => c.Index(tagIndexName));

            return indexResponse.IsValid;
        }


        public static IEnumerable<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument> GetAllNotes()
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            // récupération de tous les documents de l'index des notes
            var notes = esClient.Search<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument>(search =>
                    search.Index(noteIndexName)
                            .Size(1000)
                            .Query(q => q.MatchAll()));


            return notes.Documents.ToList();
        }

        /// <summary>
        /// Renvoie tous les tags indexés
        /// </summary>
        /// <returns>La liste des tags</returns>
        public static IEnumerable<Infrastructure.ElasticSearch.IndexDocuments.TagDocument> GetAllTags()
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            // récupération de tous les documents de l'index des notes
            var tags = esClient.Search<Infrastructure.ElasticSearch.IndexDocuments.TagDocument>(search =>
                    search.Index(tagIndexName)
                            .Size(1000)
                            .Query(q => q.MatchAll()));


            return tags.Documents.ToList();
        }

        //Renvoie toutes les notes qui contiennent un terme en commun
        public static IEnumerable<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument> SearchNotesByTitle(string title)
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            // récupération de tous les documents de l'index dont les titres de note correspond au texte dans title
            var notes = esClient.Search<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument>(search =>
                    search.Index(noteIndexName)
                            .Size(1000)
                            .Query(q => q.Match(m => m.Field(f => f.NoteTitre)
                            .Query(title))));


            return notes.Documents.ToList();
        }

        //Renvoie toutes les notes d'un utilisateur
        public static IEnumerable<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument> GetAllNotesOfUser(string userId)
        {
            var esClient = new ElasticClient(GetESConnectionSettings());
            // récupération de tous les documents de l'index des notes du user
            var notes = esClient.Search<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument>(search =>
                    search.Index(noteIndexName)
                            .Size(1000)
                            .Query(q => q.Match(m => m.Field(f => f.IdUser)
                                                     .Query(userId))));
            return notes.Documents.ToList().OrderBy(m => m.DateCreationNote);
        }

        public static IEnumerable<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument> SearchNotes(string searchQuery)
        {
            var esClient = new ElasticClient(GetESConnectionSettings());

            var notes = esClient.Search<Infrastructure.ElasticSearch.IndexDocuments.NoteDocument>(search =>
                    search.Index(noteIndexName)
                            .Size(1000)
                            .Query(q => q.MultiMatch(m => m.Query(searchQuery))));

            return notes.Documents.ToList().OrderBy(m => m.DateCreationNote);

        }
    }
}