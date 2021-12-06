using Document.API.Models;
using MongoDB.Driver;

namespace Document.API.Filters
{
    public class DoctypesFilters
    {
        public FilterDefinition<DocumentData> GenerateFilter(DocumentFilter documentData)
        {
            var fBuilder = Builders<DocumentData>.Filter;
            var filter = fBuilder.Eq(fp => fp.DocId, documentData.DocId)
                & fBuilder.Eq(fp => fp.Data.UrgencyId, documentData.Data.UrgencyId);
            return filter;
        }
    }
}
