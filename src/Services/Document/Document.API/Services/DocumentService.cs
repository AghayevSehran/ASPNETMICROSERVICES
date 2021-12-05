using Document.API.Models;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Document.API.Services
{
    public class DocumentService
    {
        private readonly IMongoCollection<DocumentData> _documets;
        public DocumentService(IDocumentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _documets = database.GetCollection<DocumentData>(settings.DocumentsCollectionName);
            ConventionRegistry.Register("Ignore",
                            new ConventionPack
                            {
                                new IgnoreIfNullConvention(true)
                            },
                            t => true);
        }
        public async Task<List<DocumentData>> GetAllAsync()
        {
            return await _documets.Find(s => s.DocId == 3).ToListAsync();
        }
        public async Task<DocumentData> GetByIdAsync(string id)
        {
            return await _documets.Find(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<DocumentData> GetByDocIdAsync(int docId)
        {
            return await _documets.Find(s => s.DocId == docId).FirstOrDefaultAsync();
        }
        public async Task<DocumentData> CreateAsync(DocumentData document)
        {
            await _documets.InsertOneAsync(document);
            return document;
        }
        public async Task UpdateAsync(int documentId, DocumentData document)
        {
            await _documets.ReplaceOneAsync(s => s.DocId == documentId, document);
        }
        public async Task DeleteAsync(string id)
        {
            await _documets.DeleteOneAsync(s => s.Id == id);
        }
    }
}
