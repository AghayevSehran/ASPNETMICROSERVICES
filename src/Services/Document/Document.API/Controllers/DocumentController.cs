using Document.API.Filters;
using Document.API.Models;
using Document.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Document.API.Controllers
{
    //https://www.mongodb.com/developer/quickstart/csharp-crud-tutorial/
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;
        private readonly DoctypesFilters _doctypesFilters;

        public DocumentController(DocumentService service, DoctypesFilters doctypesFilters)
        {
            _documentService = service;
            _doctypesFilters = doctypesFilters;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentData>>> GetAll()
        {
            var documents = await _documentService.GetAllAsync();
            return Ok(documents);
        }

 
        [HttpPost]
        [Route("~/api/[controller]/Filter")]
        public async Task<ActionResult<IEnumerable<DocumentData>>> GetAllFiltered(DocumentFilter documentData)
        {
            var documents = await _documentService.GetAllAsyncFilter(_doctypesFilters.GenerateFilter(documentData));
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
        public async Task<IActionResult> Update(int docId, DocumentData updatedData)
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
