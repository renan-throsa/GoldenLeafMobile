using GoldenLeafMobile.Models.ProductModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ProductViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public Product Product { get; set; }
        public DetailsPage(Product product)
        {
            InitializeComponent();
            Product = product;
            BindingContext = this;

        }

        private void buttonEdit_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditPage(Product));
        }
    }
}