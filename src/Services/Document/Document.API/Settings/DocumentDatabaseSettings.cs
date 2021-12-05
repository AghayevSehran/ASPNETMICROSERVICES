namespace DocumentMetadata.API.Models
{
    public class DocumentDatabaseSettings : IDocumentDatabaseSettings
    {
        public string DocumentsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
