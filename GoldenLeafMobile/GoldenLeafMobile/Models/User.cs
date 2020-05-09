using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models
{
    public abstract class User : BaseClass
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
