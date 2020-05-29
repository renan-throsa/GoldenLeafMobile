using Newtonsoft.Json;

namespace GoldenLeafMobile.Models
{
    public abstract class User : BaseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
                
    }
}
