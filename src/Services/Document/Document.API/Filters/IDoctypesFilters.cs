using Document.API.Models;
using MongoDB.Driver;

namespace Document.API.Filters
{
    public interface IDoctypesFilters
    {
        public FilterDefinition<DocumentData> GenerateFilter(DocumentFilter documentData);
    }
}
