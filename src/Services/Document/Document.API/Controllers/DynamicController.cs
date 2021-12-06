
using Document.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Linq;

namespace Document.API.Controllers
{

    [ApiController]
    public class DynamicController : ControllerBase
    {
        private readonly DocumentBsonService _documentService;
        public DynamicController(DocumentBsonService documentBsonService)
        {
            _documentService = documentBsonService;
        }
    
        [HttpPost]
        [Route("api/mongodb/add")]
        public async Task<IActionResult> Create([FromBody] JObject jsonbody)
        {
            var doc = BsonDocument.Parse(jsonbody.ToString());
            await _documentService.CreateAsync(doc);
            return Ok(doc);

        }
    }
}
