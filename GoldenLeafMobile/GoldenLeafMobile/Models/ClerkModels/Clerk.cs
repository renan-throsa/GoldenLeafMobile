using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace GoldenLeafMobile.Models.ClerkModels
{
    public class Clerk
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public bool Syncronized { get; set; }

        private Token Token { get; set; }

        public ImageSource ProfileImage { get; set; }

        public byte[] ByteImage { get; set; }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    email = Email,
                    phoneNumber = PhoneNumber,
                    photo = Convert.ToBase64String(ByteImage)
                }
           );
        }

        public string GetToken()
        {
            return this.Token.Value;
        }

        public bool IsTokenValid()
        {
            var now = DateTime.Now;

            if (now > Token.Expiration)
            {
                return false;
            }
            return true;
        }


    }
}
