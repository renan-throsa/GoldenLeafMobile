using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace GoldenLeafMobile.Models.ClerkModels
{
    public class Clerk : User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("token")]
        private Token Token { get; set; }

        public ImageSource ProfileImage { get; set; }

        public Clerk()
        {
            ProfileImage = "UserIcon.png";
        }

        public override string ToJson()
        {
            return "Not needed here.";
        }

        public string GetToken()
        {
            return this.Token.Value;
        }
        public bool IsTokenExperationTimeValid()
        {
            var now = DateTime.Now;
            if (now < Token.ExpirationTime)
            {
                return true;
            }
            return false;
        }
    }
}
