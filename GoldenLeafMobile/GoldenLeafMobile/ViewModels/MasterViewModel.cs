using GoldenLeafMobile.Media;
using GoldenLeafMobile.Models.ClerkModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        private readonly Clerk _clerk;
        private bool _editing = false;


        public ICommand EditPerfilCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand TakePictureCommand { get; private set; }


        public bool Editing
        {
            get { return _editing; }
            set { _editing = value; OnPropertyChanged(); }
        }

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

        public ImageSource ProfileImage
        {
            get { return _clerk.ProfileImage; }
            set { _clerk.ProfileImage = value; OnPropertyChanged(); }
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

            TakePictureCommand = new Command(() =>
            {
                DependencyService.Get<ICamera>().TakePicture();
            });

            MessagingCenter.Subscribe<byte[]>(this, "ProfilePicture",
                (_bytes) =>
                {
                    ProfileImage = ImageSource.FromStream(() => new MemoryStream(_bytes));                    
                });
        }

    }
}
