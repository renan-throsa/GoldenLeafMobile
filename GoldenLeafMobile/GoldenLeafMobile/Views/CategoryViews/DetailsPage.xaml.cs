using GoldenLeafMobile.Models.CategoryModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.CategoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Category category)
        {
            InitializeComponent();
        }


    }
}