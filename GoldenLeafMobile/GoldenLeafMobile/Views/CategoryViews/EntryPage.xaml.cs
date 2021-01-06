using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.ViewModels.CategoryViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.CategoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {
        public SaveViewModel ViewModel { get; set; }
        public EntryPage()
        {
            InitializeComponent();
            ViewModel = new SaveViewModel(Application.Current.Properties["Clerk"] as Clerk);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SignUpMessages();
        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<Category>(this, ViewModel.ASK, async (_category) =>
            {
                var confirm = await DisplayAlert("Salvar categoria", "Deseja mesmo salvar a categoria?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveCategory();
                }
            });

            MessagingCenter.Subscribe<Category>(this, ViewModel.SUCCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar categoria", "Categoria salva com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAIL, (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });

            MessagingCenter.Subscribe<string>(this, ViewModel.ACCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar categoria", $"{_msg} o seu token expirou! Refaça o login.", "Ok");
                await Navigation.PopToRootAsync();
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Category>(this, ViewModel.SUCCESS);
            MessagingCenter.Unsubscribe<Category>(this, ViewModel.ASK);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);
            MessagingCenter.Unsubscribe<string>(this, ViewModel.ACCESS);
        }
    }
}