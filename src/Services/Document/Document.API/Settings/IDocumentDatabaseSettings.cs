namespace Document.API.Models
{
    public interface IDocumentDatabaseSettings
    {     
        string DocumentsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
