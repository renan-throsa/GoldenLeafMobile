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
        public ObservableCollection<OrderTableItem> Items { get; private set; }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            private set { _description = value; OnPropertyChanged(); ((Command)AddProductComand).ChangeCanExecute(); }
        }

        private float _unitCost;
        public float UnitCost
        {
            get { return _unitCost; }
            private set { _unitCost = value; OnPropertyChanged(); ((Command)AddProductComand).ChangeCanExecute(); }
        }

        private float _extendedCost;
        public float ExtendedCost
        {
            get { return UnitCost * Quantity; }
            private set { _extendedCost = value; OnPropertyChanged(); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
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
                AddItem();
                CleanTable();

            }, () =>
            {
                return Id > 0
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
            Id = item.Id;
            Description = item.Description;
            UnitCost = item.UnitCost;
            ExtendedCost = item.ExtendedCost;
            Quantity = item.Quantity;            
            IsSearching = false;
            IsEditing = true;
        }

        internal void Remove(OrderTableItem tableItem)
        {
            Items.Remove(tableItem);
        }

        private void CleanTable()
        {
            IsSearching = true;
            IsEditing = false;
            Id = 0;
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
            Id = partialProduct.Id;
            IsSearching = false;
            IsEditing = true;
        }

        private void AddItem()
        {
            foreach (var item in Items)
            {
                //Compere the id's.
                if (item.Id == Id)
                {
                    var index = Items.IndexOf(item);
                    Items[index] = new OrderTableItem(Id, Description, UnitCost, Quantity, ExtendedCost);
                    return;
                }
            }
            Items.Add(new OrderTableItem(Id, Description, UnitCost, Quantity, ExtendedCost));
        }
    }
}
