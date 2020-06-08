using GoldenLeafMobile.Models.CategoryModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.CategoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public Category Category { get; }

        public DetailsPage(Category category)
        {
            InitializeComponent();
            Category = category;
            BindingContext = this;
        }

        private void buttonEdit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditPage(Category));
        }
    }
}