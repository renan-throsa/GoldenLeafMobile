using GoldenLeafMobile.Models.ClerkModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
       

        public MasterDetailView(Clerk clerk)
        {
            InitializeComponent();
            this.Master = new MasterView(clerk);
        }
    }
}