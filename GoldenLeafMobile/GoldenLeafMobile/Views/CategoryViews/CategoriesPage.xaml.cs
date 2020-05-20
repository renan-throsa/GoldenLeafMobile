using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.ViewModels.CategoryViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.CategoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        public ListViewModel ViewModel { get; set; }

        public CategoriesPage()
        {
            InitializeComponent();
            ViewModel = new ListViewModel();
            BindingContext = ViewModel;            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            categoriesListView.SelectedItem = false;
            MessagingCenter.Subscribe<Category>(this, "SelectedCategory",
                (_category) => Navigation.PushAsync(new DetailsPage(_category)));

            await ViewModel.GetCategories();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Category>(this, "SelectedCategory");
        }

        private void OnEdit(object sender, System.EventArgs e)
        {

        }

        private void OnNewProduct(object sender, System.EventArgs e)
        {

        }

        private void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}