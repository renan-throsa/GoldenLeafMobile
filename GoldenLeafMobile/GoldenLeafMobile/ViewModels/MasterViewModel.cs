using GoldenLeafMobile.Models.ClerkModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.ViewModels
{
    public class MasterViewModel
    {
        private readonly Clerk _clerk;

        public string Name
        {
            get { return _clerk.Name; }
            set { _clerk.Name = value; }
        }

        public string Email
        {
            get { return _clerk.Email; }
            set { _clerk.Email = value; }
        }

        public string Phone
        {
            get { return _clerk.PhoneNumber; }
            set { _clerk.PhoneNumber = value; }
        }


        public MasterViewModel(Clerk clerk)
        {
            _clerk = clerk;
        }

    }
}
