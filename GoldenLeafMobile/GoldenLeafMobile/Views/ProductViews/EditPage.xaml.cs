using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ProductModels;
using GoldenLeafMobile.ViewModels.ProductViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ProductViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        public EditViewModel ViewModel { get; private set; }
        public EditPage(Product product)
        {
            InitializeComponent();
            ViewModel = new EditViewModel(Application.Current.Properties["Clerk"] as Clerk,product);
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.GetCategories();
            ViewModel.SetCategoryIndex();
            SignUpMessages();
        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<Product>(this, ViewModel.ASK, async (_msg) =>
            {
                var confirm = await DisplayAlert("Salvar produto", "Deseja mesmo salvar o produto?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveProduct();
                }
            });

            MessagingCenter.Subscribe<Product>(this, ViewModel.SUCCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar produto", "Produto salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAIL, async (_msg) =>
            {
                await DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<string>(this, ViewModel.ACCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar produto", $"{_msg} o seu token expirou! Refaça o login.", "Ok");
                await Navigation.PopToRootAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Product>(this, ViewModel.ASK);
            MessagingCenter.Unsubscribe<Product>(this, ViewModel.SUCCESS);
            MessagingCenter.Unsubscribe<string>(this, ViewModel.ACCESS);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);
        }

    }
}