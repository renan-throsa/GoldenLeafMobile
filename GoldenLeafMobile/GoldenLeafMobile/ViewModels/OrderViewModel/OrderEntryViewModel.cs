using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
using GoldenLeafMobile.Models.OrderModels;
using Newtonsoft.Json;
using System;
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
            private set { CurrentItem.Description = value; OnPropertyChanged(); ((Command)AddProductComand).ChangeCanExecute(); }
        }

        public float UnitCost
        {
            get { return CurrentItem.UnitCost; }
            private set { CurrentItem.UnitCost = value; OnPropertyChanged(); ((Command)AddProductComand).ChangeCanExecute(); }
        }

        public float ExtendedCost
        {
            get { return CurrentItem.GetExtendedCost(); }
            private set { CurrentItem.ExtendedCost = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get { return CurrentItem.Quantity; }
            set
            {
                CurrentItem.Quantity = value;
                OnPropertyChanged();
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
            get { return _searching; }
            set { _searching = value; OnPropertyChanged(); }
        }

        private bool _editing = false;
        public bool IsEditing
        {
            get { return _editing; }
            set { _editing = value; OnPropertyChanged(); }
        }

        public OrderEntryViewModel(Client client)
        {
            Items = new ObservableCollection<OrderTableItem>();
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
                CleanTable();

            }, () =>
            {
                return CurrentItem.Id > 0
                && !string.IsNullOrEmpty(Description)
                && UnitCost > 0
                && Quantity > 0
                && ExtendedCost > 0;
            }

            );

            SaveOrderComand = new Command(() => { }, () => { return Items.Count >= 1; });

        }

        private async void GetProduct()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(URL_PRODUCT + Code);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var partialProduct = JsonConvert.DeserializeObject<PartialProduct>(result);
                    FillOutTable(partialProduct);
                }
            }
        }

        public async void SaveOrder()
        {
            using (HttpClient httpClient = new HttpClient())
            {

            }
        }

        internal void Edit(OrderTableItem item)
        {
            Description = item.Description;
            UnitCost = item.UnitCost;
            ExtendedCost = item.ExtendedCost;
            Quantity = item.Quantity;
            CurrentItem = item;
            IsSearching = false;
            IsEditing = true;
        }

        internal void Remove(OrderTableItem tableItem)
        {
            foreach (var item in Items)
            {
                if (item.Id == tableItem.Id)
                {
                    Items.Remove(item);
                }
            }
        }

        private void CleanTable()
        {
            IsSearching = true;
            IsEditing = false;
            CurrentItem = new OrderTableItem();
            Code = "";
            Description = "";
            Quantity = 0;
            UnitCost = 0;
            ExtendedCost = 0;

        }

        private void FillOutTable(PartialProduct partialProduct)
        {
            Description = partialProduct.Description;
            UnitCost = partialProduct.UnitCost;
            CurrentItem.Id = partialProduct.Id;
            IsSearching = false;
            IsEditing = true;
        }
    }
}
