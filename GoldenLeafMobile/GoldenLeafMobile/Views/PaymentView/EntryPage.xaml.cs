using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClientModels;
using GoldenLeafMobile.ViewModels.PaymentViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.PaymentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {
        public PaymentEntryViewModel ViewModel { get; private set; }

        public EntryPage(Client client)
        {
            InitializeComponent();
            ViewModel = new PaymentEntryViewModel(client);
            BindingContext = ViewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            SignUpMessages();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, ViewModel.ASK);
            MessagingCenter.Unsubscribe<Client>(this, ViewModel.SUCCESS);
            MessagingCenter.Unsubscribe<string>(this, ViewModel.ACCESS);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);

        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<string>(this, ViewModel.ASK, async (_amount) =>
            {
                var confirm = await DisplayAlert("Salvar pagamento", $"Deseja efetuar o pagamento de {_amount}?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SavePayment();
                }
            });

            MessagingCenter.Subscribe<string>(this, ViewModel.SUCCESS, async (_msg) =>
            {
                await DisplayAlert("Pagamento salvo", $" O total da sua conta agora é de {_msg}", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAIL, (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });

            MessagingCenter.Subscribe<string>(this, ViewModel.ACCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar pagamento", $"{_msg} o seu token expirou! Refaça o login.", "Ok");
                await Navigation.PopToRootAsync();
            });
        }
    }
}