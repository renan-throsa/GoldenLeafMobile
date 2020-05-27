using GoldenLeafMobile.Models;
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
        protected readonly string SUCCESS = "SuccessSavingProduct";
        protected readonly string FAIL = "FailedSavingProduct";
        protected readonly string ASK = "SavingProduct";
        public EditPage(Product product)
        {
            InitializeComponent();
            ViewModel = new EditViewModel(product);
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
            MessagingCenter.Subscribe<Product>(this, ASK, async (_msg) =>
            {
                var confirm = await DisplayAlert("Salvar produto", "Deseja mesmo salvar o produto?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveProduct();
                }
            });

            MessagingCenter.Subscribe<Product>(this, SUCCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar produto", "Produto salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, FAIL, (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Product>(this, SUCCESS);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, FAIL);
        }

    }
}