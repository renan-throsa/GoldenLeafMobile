using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.CategoryModels
{
    public class Category:BaseClass
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
