using GoldenLeafMobile.Models.ClerkModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        private readonly Clerk _clerk;
        private bool _editing = false;

        public bool Editing
        {
            get { return _editing; }
            set { _editing = value; OnPropertyChanged(); }
        }

        public ICommand EditPerfilCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }


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

        public string PhoneNumber
        {
            get { return _clerk.PhoneNumber; }
            set { _clerk.PhoneNumber = value; }
        }


        public MasterViewModel(Clerk clerk)
        {
            _clerk = clerk;
            EditPerfilCommand = new Command(() =>
            {
                MessagingCenter.Send<Clerk>(_clerk, "EditingClerk");
            });

            SaveCommand = new Command(() =>
            {
                Editing = false;
                MessagingCenter.Send<Clerk>(_clerk, "SaveEditedClerk");
            });

            EditCommand = new Command(() =>
            {
                Editing = true;
            });
        }

    }
}
