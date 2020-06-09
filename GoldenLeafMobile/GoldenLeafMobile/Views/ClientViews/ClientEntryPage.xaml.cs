using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClientModels;
using GoldenLeafMobile.ViewModels.ClientViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ClientViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientEntryPage : ContentPage
    {
        public ClientEntryViewModel ViewModel { get; private set; }

        public ClientEntryPage()
        {
            InitializeComponent();
            ViewModel = new ClientEntryViewModel(new Client());
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SignUpMessages();
        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<Client>(this, ViewModel.ASK, async (_client) =>
            {
                var confirm = await DisplayAlert("Salvar cliente", "Deseja mesmo salvar o cliente?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveClient();
                }
            });

            MessagingCenter.Subscribe<Client>(this, ViewModel.SUCCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar cliente", "Cliente salvo com sucesso!", "Ok");
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
            MessagingCenter.Unsubscribe<Client>(this, ViewModel.ASK);
            MessagingCenter.Unsubscribe<Client>(this, ViewModel.SUCCESS);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);
        }

    }
}