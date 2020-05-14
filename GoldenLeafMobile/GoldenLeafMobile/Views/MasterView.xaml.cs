using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : ContentPage
    {

        public MasterViewModel ViewModel { get; set; }

        public MasterView(Clerk clerk)
        {
            InitializeComponent();
            ViewModel = new MasterViewModel(clerk);
            this.BindingContext = ViewModel;
        }


    }
}