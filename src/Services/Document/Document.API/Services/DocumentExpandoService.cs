using Document.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Document.API.Services
{
    public class DocumentExpandoService
    {
        private readonly IMongoCollection<DocumentDataExpando> _documets;
        public DocumentExpandoService(IDocumentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _documets = database.GetCollection<DocumentDataExpando>(settings.DocumentsCollectionName);
        }
        public async Task<DocumentDataExpando> CreateAsync(DocumentDataExpando document)
        {
            await _documets.InsertOneAsync(document);
            return document;
        }
        public async Task<List<DocumentDataExpando>> GetAllAsync()
        {
            return await _documets.Find(s => s.DocId == 4000).ToListAsync();
        }

        internal async Task<List<DocumentDataExpando>> GetAllAsyncFiltered()
        {
            return await _documets.Find(s => s.DocId == 4000).ToListAsync();
        }

        public async Task<DocumentDataExpando> GetByIdAsync(string id)
        {
            return await _documets.Find(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<DocumentDataExpando> GetByDocIdAsync(int docId)
        {
            return await _documets.Find(s => s.DocId == docId).FirstOrDefaultAsync();
        } 
        public async Task UpdateAsync(int documentId, DocumentDataExpando document)
        {
            await _documets.ReplaceOneAsync(s => s.DocId == documentId, document);
        }
        public async Task DeleteAsync(string id)
        {
            await _documets.DeleteOneAsync(s => s.Id == id);
        }

    }
}
