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


        public Client Client { get; }
        public Clerk Clerk { get; }
        public OrderTableItem CurrentItem { get; private set; }
        public ObservableCollection<OrderTableItem> Items { get; private set; }

        public string Description
        {
            get { return CurrentItem.Description; }
            private set { CurrentItem.Description = value; ((Command)AddProductComand).ChangeCanExecute(); }
        }

        public float UnitCost
        {
            get { return CurrentItem.UnitCost; }
            private set { CurrentItem.UnitCost = value; ((Command)AddProductComand).ChangeCanExecute(); }
        }

        public float ExtendedCost
        {
            get { return CurrentItem.GetExtendedCost(); }
        }

        public int Quantity
        {
            get { return CurrentItem.Quantity; }
            set
            {
                CurrentItem.Quantity = value;
                OnPropertyChanged("ExtendedCost");
                ((Command)AddProductComand).ChangeCanExecute();
            }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(); ((Command)SearchProductComand).ChangeCanExecute(); }
        }

        private bool _searching = true;
        public bool IsSearching
        {
            get { return _searching; OnPropertyChanged(); }
            set { _searching = value; }
        }

        private bool _editing = false;
        public bool IsEditing
        {
            get { return _editing; }
            set { _editing = value; }
        }

        public OrderEntryViewModel(Client client)
        {
            CurrentItem = new OrderTableItem();
            Client = client;
            Clerk = Application.Current.Properties["Clerk"] as Clerk;

            SearchProductComand = new Command(
                () =>
                    {
                        GetProduct();
                    },
                () =>
                    {
                        return !string.IsNullOrEmpty(Code) && (Code.Length >= 9 && Code.Length <= 13);
                    });

            AddProductComand = new Command(
                () =>
            {
                Items.Add(CurrentItem);
                IsSearching = true;
                IsEditing = false;
                CurrentItem = new OrderTableItem();
                Code = "";
            }, () =>
            {
                return CurrentItem.Id > 0
                && !string.IsNullOrEmpty(Description)
                && UnitCost > 0
                && Quantity > 0
                && ExtendedCost > 0;
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
                    IsSearching = false;
                    IsEditing = true;
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
