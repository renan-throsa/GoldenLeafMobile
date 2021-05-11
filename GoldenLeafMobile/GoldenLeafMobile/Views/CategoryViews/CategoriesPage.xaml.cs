using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.Models.ProductModels;
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

            MessagingCenter.Subscribe<Category>(this, ViewModel.SELECTED,
                (_category) => Navigation.PushAsync(new DetailsPage(_category)));

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAIL,
               (_msg) =>
               {
                   DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
               });

            if (ViewModel.Entities.Count == 0)
            {
                await ViewModel.GetEntities();
            }
            else
            {
                listView.SelectedItem = false;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Category>(this, ViewModel.SELECTED);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);
        }

        private void OnEdit(object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Navigation.PushAsync(new EditPage(mi.CommandParameter as Category));
        }

        private void OnNewProduct(object sender, System.EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var c = mi.CommandParameter as Category;
            var p = new Product { CategoryId = c.Id };
            Navigation.PushAsync(new ProductViews.EditPage(p));
        }


        private async void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchString = $"Title={searchField.Text}";
            await ViewModel.GetEntities(page: 1, parameter: searchString);
        }


        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EntryPage());
        }
    }
}