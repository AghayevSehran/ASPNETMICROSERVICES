namespace Document.API.Filters
{
    public class DocumentFilter
    {
        public int DocId { get; set; }
        public int Doctype { get; set; }
        public DocumentAdditionFilter Data { get; set; }
    }
}
