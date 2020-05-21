using GoldenLeafMobile.Models.ProductModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ProductViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        
        public EditPage(Product product)
        {
            InitializeComponent();
            
        }

        
    }
}