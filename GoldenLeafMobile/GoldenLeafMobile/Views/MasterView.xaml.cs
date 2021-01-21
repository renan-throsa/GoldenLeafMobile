using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : TabbedPage
    {
        public MasterViewModel ViewModel { get; set; }

        public MasterView(Clerk clerk)
        {
            InitializeComponent();
            ViewModel = new MasterViewModel(clerk);
            this.BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SignUpMessages();

        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<Clerk>(this, ViewModel.ASK, async (_mgs) =>
            {
                var confirm = await DisplayAlert("Salvar atendente", "Deseja mesmo salvar o atendente?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveClerk();
                }

            });

            MessagingCenter.Subscribe<Clerk>(this, ViewModel.SUCCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar atendente", "Atendente salvo com sucesso!", "Ok");
                this.CurrentPage = this.Children[0];
            });

            MessagingCenter.Subscribe<string>(this, ViewModel.ACCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar cliente", $"{_msg} o seu token expirou! Refaça o login.", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAIL, (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Clerk>(this, ViewModel.ASK);
            MessagingCenter.Unsubscribe<Clerk>(this, ViewModel.SUCCESS);
            MessagingCenter.Unsubscribe<string>(this, ViewModel.ACCESS);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);
        }
    }
}