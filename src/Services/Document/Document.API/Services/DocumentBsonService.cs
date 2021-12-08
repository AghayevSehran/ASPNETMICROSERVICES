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
        private  BsonDocument CreateIdFilter(string id)
        {
            return new BsonDocument("_id", new BsonObjectId(new ObjectId(id)));
        }
        public async Task<BsonDocument> GetByIdAsync(string id)
        {
            return await _documets.Find(CreateIdFilter(id)).SingleAsync();
        }
        public async Task<BsonDocument> GetByDocIdAsync(int docId)
        {
            var filter = new BsonDocument { { "DocId", docId } };
            return await _documets.Find(filter).SingleAsync();
        }
        public async Task<UpdateResult> CreateOrUpdateField(string id, string fieldName, string value)
        {        
            var update = Builders<BsonDocument>.Update.Set(fieldName, new BsonString(value));
            return await _documets.UpdateOneAsync(CreateIdFilter(id), update);
        }
         
        public async Task<ReplaceOneResult> UpdateAsync(int docId,BsonDocument document)
        {
            var filter = new BsonDocument { { "DocId", docId } };
           return await _documets.ReplaceOneAsync(filter, document);
        }

        public async Task<DeleteResult> DeleteDocument(int docId)
        {
            var filter = new BsonDocument { { "DocId", docId } };
            return await _documets.DeleteOneAsync(filter);
        }

        public async Task<List<BsonDocument>> GetDocuments(int page,int pagesize, BsonDocument document)
        {          
            var filterBuilder = Builders<BsonDocument>.Filter;
            var sort = Builders<BsonDocument>.Sort.Descending("_id");
            var result = await _documets.Find(document)
                                         .Skip((page - 1)  * pagesize)
                                         .Limit(pagesize)
                                         .Sort(sort).ToListAsync();
            return result;
        }

        public async Task<List<BsonDocument>> GetDocumentsProjection(int page, int pagesize, BsonDocument document, List<string> projection)
        {
            //var filterBuilder = Builders<BsonDocument>.Filter;
            var sort = Builders<BsonDocument>.Sort.Descending("_id");

            var fileds = Builders<BsonDocument>.Projection.Include(projection?.First());
            foreach (var field in projection.Skip(1))
            {
                fileds = fileds.Include(field);
            }

            var result = await _documets.Find(document).Project(fileds)
                                         .Skip((page - 1) * pagesize)
                                         .Limit(pagesize)
                                         .Sort(sort).ToListAsync();
            return result;
        }

        public async Task<long> GetCollectionCount()
        {
            return await _documets.EstimatedDocumentCountAsync();
        }



    }
}
