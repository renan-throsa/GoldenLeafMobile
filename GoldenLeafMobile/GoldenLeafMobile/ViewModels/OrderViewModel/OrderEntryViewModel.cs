using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
using GoldenLeafMobile.Models.OrderModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.OrderViewModel
{
    public class OrderEntryViewModel : BaseViewModel
    {
        public ICommand SearchProductComand { get; private set; }
        public ICommand AddProductComand { get; private set; }
        public ICommand SaveOrderComand { get; private set; }

        private readonly string URL_PRODUCT = "https://golden-leaf.herokuapp.com/api/product/code/";
        public readonly string SEARCH = "SearchProduct";
        private bool _searching = false;
        private bool _editing = false;
        private string _code;
        private float _quantity;

        public Client Client { get; }
        public Clerk Clerk { get; }

        public string Description { get; set; }
        public float UnitCost { get; set; }
        public float ExtendedCost { get; set; }

        

        public float Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        public OrderTableItem CurrentItem { get; private set; }
        public ObservableCollection<OrderTableItem> Items { get; private set; }

        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(); }
        }

        public bool Searching
        {
            get { return _searching; }
            set { _searching = value; OnPropertyChanged(); }
        }

        public bool Editing
        {
            get { return _editing; }
            set { _editing = value; OnPropertyChanged(); }
        }

        public OrderEntryViewModel(Client client)
        {
            Client = client;
            Clerk = Application.Current.Properties["Clerk"] as Clerk;
            SearchProductComand = new Command(
                () =>
                    {
                        MessagingCenter.Send(this, SEARCH);
                    },
                () =>
                    {
                        return !string.IsNullOrEmpty(Code);
                    });
            AddProductComand = new Command(
                () =>
            {
                Items.Add(CurrentItem);
            }, () =>
            {
                return CurrentItem.Id > 0 &&
                !string.IsNullOrEmpty(CurrentItem.Description)
                && CurrentItem.UnitCost > 0
                && CurrentItem.Quantity > 0
                && CurrentItem.ExtendedCost > 0;
            }

            );
            SaveOrderComand = new Command(() => { });

        }

        private async void GetProduct()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(URL_PRODUCT + Code);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    CurrentItem = JsonConvert.DeserializeObject<OrderTableItem>(result);
                }
            }
        }

        public async void SaveOrder()
        {
            using (HttpClient httpClient = new HttpClient())
            {

            }
        }

    }
}
