using GoldenLeafMobile.Models.PaymentModel;
using GoldenLeafMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.GetEntities();
        }
    }
}