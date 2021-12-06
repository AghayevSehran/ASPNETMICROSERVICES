using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Document.API.Models
{
    [BsonIgnoreExtraElements]
    public class DocumentData
    {   
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnore]
        public string Id { get; set; }
        public int DocId { get; set; }
        public DocumentAddition Data { get; set; }
    }
}
