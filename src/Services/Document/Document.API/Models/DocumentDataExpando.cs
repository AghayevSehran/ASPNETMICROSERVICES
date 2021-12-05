using DocumentMetadata.API.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Dynamic;

namespace DocumentMetadata.API.Models
{
    public class DocumentDataExpando
    {
       
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int DocId { get; set; }
        public ExpandoObject Data { get; set; }
    }
}
