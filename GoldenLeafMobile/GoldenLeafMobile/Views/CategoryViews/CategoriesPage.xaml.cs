using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.CategoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        public ListViewModel<Category> ViewModel { get; set; }

        public CategoriesPage()
        {
            InitializeComponent();
            ViewModel = new ListViewModel<Category>();
            BindingContext = ViewModel;            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listView.SelectedItem = false;
            MessagingCenter.Subscribe<Category>(this, "SelectedCategory",
                (_category) => Navigation.PushAsync(new DetailsPage(_category)));

            await ViewModel.GetEntities();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Category>(this, "SelectedCategory");
        }

        private void OnEdit(object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Navigation.PushAsync(new EditPage(mi.CommandParameter as Category));
        }

        private void OnNewProduct(object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete", mi.CommandParameter.ToString(), "OK");
        }

        private void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CategoryEntryPage());
        }
    }
}