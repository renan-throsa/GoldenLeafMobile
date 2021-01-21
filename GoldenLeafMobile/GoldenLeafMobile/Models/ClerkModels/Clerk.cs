using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;

namespace GoldenLeafMobile.Models.ClerkModels
{
    public class Clerk : User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("token")]
        private Token Token { get; set; }

        [JsonProperty("profile_pic")]
        public string StringImage { get; set; }

        public ImageSource ProfileImage { get; set; }

        public byte[] ByteImage { get; set; }
                       

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    email = Email,
                    phone_number = PhoneNumber,
                    image_file = Convert.ToBase64String(ByteImage)
                }
           );
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
