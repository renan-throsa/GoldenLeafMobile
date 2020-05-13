using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.ClerkModels
{
    class LoginException : Exception
    {
        public string ReasonPhrase { get; set; }
        public LoginException(string reasonPhrase, string message) : base(message)
        {
            ReasonPhrase = reasonPhrase;
        }

    }
}
