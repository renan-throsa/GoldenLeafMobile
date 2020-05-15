using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.ClerkModels
{
    public class Clerk : User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        public string ProfileImage { get; set; }


    }
}
