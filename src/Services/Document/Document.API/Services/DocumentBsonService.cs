using Document.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Document.API.Services
{
    public class DocumentBsonService
    {
        private readonly IMongoCollection<BsonDocument> _documets;
        public DocumentBsonService(IDocumentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _documets = database.GetCollection<BsonDocument>(settings.DocumentsCollectionName);
        }
        public async Task<BsonDocument> CreateAsync(BsonDocument document)
        {
            await _documets.InsertOneAsync(document);
            return document;
        }
        public async Task<BsonDocument> CreateAsyncExpando(BsonDocument document)
        {
            await _documets.InsertOneAsync(document);
            return document;
        }
    }
}
