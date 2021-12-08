using Newtonsoft.Json.Linq;

namespace Document.API.Models
{
    public class DocumentFilterProjection
    {
        public JObject Filter { get; set; }
        public List<string> Project { get; set; }
    }
}
