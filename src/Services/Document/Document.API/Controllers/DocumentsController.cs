using DocumentMetadata.API.Models;
using DocumentMetadata.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMetadata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentsController(DocumentService service)
        {
            _documentService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentData>>> GetAll()
        {
            var documents = await _documentService.GetAllAsync();
            return Ok(documents);
        }

        [HttpGet("{docId}")]
        public async Task<ActionResult<DocumentData>> GetById(int docId)
        {
            var document = await _documentService.GetByDocIdAsync(docId);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentData document)
        {
            await _documentService.CreateAsync(document);
            return Ok(document);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int docId, DocumentData updatedStudent)
        {
            var queriedDocument = await _documentService.GetByDocIdAsync(docId);
            if (queriedDocument == null)
            {
                return NotFound();
            }
            await _documentService.UpdateAsync(docId, updatedStudent);
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
