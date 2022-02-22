
using Document.API.Models;
using Document.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Linq;

namespace Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicController : ControllerBase
    {
        private readonly DocumentBsonService _documentService;
        public DynamicController(DocumentBsonService documentBsonService)
        {
            _documentService = documentBsonService;
        }

        [HttpPost]
        [Route("~/api/[controller]/GetWithFIlter")]
        public async Task<ActionResult> GetAll(int page,int size, [FromBody] JObject jsonbody)
        {
            var docFiler = BsonDocument.Parse(jsonbody.ToString());
            var documents = await _documentService.GetDocuments(page,size, docFiler);
            return Ok(documents);
        }

        [HttpPost]
        [Route("~/api/[controller]/GetWithSellectedColumns")]
        public async Task<ActionResult> GetAllProjection(int page, int size, [FromBody] DocumentFilterProjection documentFilterProjection)
        {
            var docFilter = BsonDocument.Parse(documentFilterProjection.Filter.ToString());
            var documents = await _documentService.GetDocumentsProjection(page, size, docFilter, documentFilterProjection.Project);
            return Ok(documents);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JObject jsonbody)
        {
            var doc = BsonDocument.Parse(jsonbody.ToString());
            await _documentService.CreateAsync(doc);
            return Ok(doc);

        }

        [HttpGet]
        [Route("~/api/[controller]/GetByDocId")]
        public async Task<ActionResult> GetByDocId(int docId)
        {
            var document = await _documentService.GetByDocIdAsync(docId);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        [HttpGet]
        [Route("~/api/[controller]/GetById")]
        public async Task<ActionResult> GetById(string id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int docId, [FromBody] JObject updatedData)
        {
            var queriedDocument = await _documentService.GetByDocIdAsync(docId);
            if (queriedDocument == null)
            {
                return NotFound();
            }
            var doc = BsonDocument.Parse(updatedData.ToString());
            var result = await _documentService.UpdateAsync(docId, doc);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int docId)
        {         
            var result = await _documentService.DeleteDocument(docId);
            return Ok(result);
        }

    }
}
