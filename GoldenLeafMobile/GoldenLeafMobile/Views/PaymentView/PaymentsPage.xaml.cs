using GoldenLeafMobile.Models.PaymentModel;
using GoldenLeafMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.PaymentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentsPage : ContentPage
    {
        public ListViewModel<Payment> ViewModel { get; set; }
        public PaymentsPage()
        {
            InitializeComponent();
            ViewModel = new ListViewModel<Payment>();
            ViewModel.AddChoises("Cliente", "Atendente");
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel.Entities.Count == 0)
            {
                await ViewModel.GetEntities();
            }
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            picker.Focus();
        }

        private async void SearchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchString = $"?{ViewModel.SearchBy}={searchField.Text}";
            await ViewModel.GetEntities(parameter: searchString);
        }
    }
}