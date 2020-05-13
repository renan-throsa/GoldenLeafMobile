using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.ClerkModels
{
    class Clerk : User
    {
        public Clerk(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
