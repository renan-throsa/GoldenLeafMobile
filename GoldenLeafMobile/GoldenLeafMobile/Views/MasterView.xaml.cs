using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : TabbedPage
    {

        public MasterView(Clerk clerk)
        {
            InitializeComponent();
            this.BindingContext = new MasterViewModel(clerk);            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SignUpMessages();       

        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<Clerk>(this, "EditingClerk", (_mgs) =>
            {
                this.CurrentPage = this.Children[1];
            });
            MessagingCenter.Subscribe<Clerk>(this, "SaveEditedClerk", (_mgs) =>
            {
                this.CurrentPage = this.Children[0];
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Clerk>(this, "EditingClerk");
            MessagingCenter.Unsubscribe<Clerk>(this, "SaveEditedClerk");
        }
    }
}