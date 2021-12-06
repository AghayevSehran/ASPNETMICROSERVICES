using Document.API.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Dynamic;

namespace Document.API.Models
{
    [BsonIgnoreExtraElements]
    public class DocumentDataExpando
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnore]
        public string Id { get; set; }
        public int DocId { get; set; }
        public Dictionary<string,string> Data { get; set; }
    }
}
