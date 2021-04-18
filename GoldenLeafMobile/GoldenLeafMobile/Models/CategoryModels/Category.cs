using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.CategoryModels
{
    public class Category : BaseModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        public Category()
        {
            Title = "";
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(
              new
              {
                  id = Id,
                  title = Title
              }
              );
        }
    }
}
