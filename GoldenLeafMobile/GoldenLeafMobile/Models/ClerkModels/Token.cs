using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.ClerkModels
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}
