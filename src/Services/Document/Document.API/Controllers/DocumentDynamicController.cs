using Document.API.Models;
using Document.API.Requests;
using Document.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace Document.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentDynamicController : ControllerBase
    {
        private readonly DocumentExpandoService _documentService;
        public DocumentDynamicController(DocumentExpandoService documentService)
        {
            _documentService = documentService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(DocumentDataExpando data)
        {
            await _documentService.CreateAsync(data);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDataExpando>>> GetAll()
        {
            var documents = await _documentService.GetAllAsync();
            return Ok(documents);
        }

        [HttpGet("{docId}")]
        public async Task<ActionResult<DocumentDataExpando>> GetById(int docId)
        {
            var document = await _documentService.GetByDocIdAsync(docId);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }


        [HttpPut]
        public async Task<IActionResult> Update(int docId, DocumentDataExpando updatedData)
        {
            var queriedDocument = await _documentService.GetByDocIdAsync(docId);
            if (queriedDocument == null)
            {
                return NotFound();
            }
            await _documentService.UpdateAsync(docId, updatedData);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var _document = await _documentService.GetByIdAsync(id);
            if (_document == null)
            {
                return NotFound();
            }
            await _documentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
