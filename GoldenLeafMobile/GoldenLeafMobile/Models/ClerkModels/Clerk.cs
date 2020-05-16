using Newtonsoft.Json;
using Xamarin.Forms;

namespace GoldenLeafMobile.Models.ClerkModels
{
    public class Clerk : User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        public ImageSource ProfileImage { get; set; }

        public Clerk()
        {
            ProfileImage = "noimage.png";
        }

    }
}
