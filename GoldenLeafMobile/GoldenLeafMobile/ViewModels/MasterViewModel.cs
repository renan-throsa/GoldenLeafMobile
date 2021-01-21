using GoldenLeafMobile.Data;
using GoldenLeafMobile.Media;
using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Service;
using System;
using System.IO;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        private readonly Clerk _clerk;
        private bool _editing = false;
        public readonly string ASK = "OnAskSaveEditedClerk";
        public readonly string SUCCESS = "OnSuccessSavingClerk";
        public readonly string FAIL = "OnFailedSavingClerk";
        public readonly string ACCESS = "OnRequestUnauthorized";


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

            SaveCommand = new Command(() =>
            {
                Editing = false;
                MessagingCenter.Send<Clerk>(_clerk, ASK);
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
                    _clerk.ByteImage = _bytes;
                    ProfileImage = ImageSource.FromStream(() => new MemoryStream(_bytes));                    
                });
        }

        internal async void SaveClerk()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var api = new ApiService<Clerk>(httpClient);
                var response = await api.PutEntityAsync(_clerk.GetToken(), _clerk.ToJson());

                if (response.IsSuccessStatusCode)
                {
                    _clerk.Syncronized = true;
                    MessagingCenter.Send<Clerk>(_clerk, SUCCESS);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                        response.Content.Dispose();

                    MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                        FAIL);

                }

            }
            
        }
        

    }
}
