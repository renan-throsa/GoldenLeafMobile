using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
using GoldenLeafMobile.Models.OrderModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.OrderViewModel
{
    public class OrderEntryViewModel : BaseViewModel
    {
        public ICommand SearchProductComand { get; private set; }
        public ICommand AddProductComand { get; private set; }
        public ICommand SaveOrderComand { get; private set; }

        public Clerk Clerk { get; set; }
        public readonly string SUCCESS = "OnSuccessAction";
        public readonly string FAIL = "OnFailedAction";
        public readonly string ASK = "OnAskBeforeAction";
        public readonly string ACCESS = "OnRequestUnauthorized";


        private readonly string URL_PRODUCT = "https://goldenleafapi.herokuapp.com/api/v1.0/Product/code/";
        private readonly string URL_ORDER = "https://goldenleafapi.herokuapp.com/api/v1.0/Order";

        public Client Client { get; }

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

        public OrderEntryViewModel(Clerk clerk, Client client)
        {
            Client = client;
            Clerk = clerk;
            Items = new ObservableCollection<OrderTableItem>();
            
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
                ClearTable();

            }, () =>
            {
                return Id > 0
                && !string.IsNullOrEmpty(Description)
                && UnitCost > 0
                && Quantity > 0
                && ExtendedCost > 0;
            }

            );

            SaveOrderComand = new Command(() =>
                {
                    MessagingCenter.Send(this, ASK);
                },
                    () => { return Items.Count >= 1; }
               );

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
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null) { response.Content.Dispose(); }

                    var simpleHttpResponse = new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content);
                    MessagingCenter.Send(simpleHttpResponse, FAIL);
                }
            }
        }

        public async void SaveOrder()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var order = CreateOrderString();
                //var encoded = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(Clerk.Token + ":" + ""));
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Clerk.GetToken()}");
                var stringContent = new StringContent(order, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(URL_ORDER, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    MessagingCenter.Send(this, SUCCESS);
                    Items.Clear();
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                        response.Content.Dispose();

                    var simpleHttpResponse = new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content);
                    MessagingCenter.Send(simpleHttpResponse, FAIL);
                }

            }
        }

        internal void Edit(OrderTableItem item)
        {
            Id = item.ProductId;
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
            ((Command)SaveOrderComand).ChangeCanExecute();
        }

        private void ClearTable()
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
                if (item.ProductId == Id)
                {
                    var index = Items.IndexOf(item);
                    Items[index] = new OrderTableItem(Id, Description, UnitCost, Quantity, ExtendedCost);
                    ((Command)SaveOrderComand).ChangeCanExecute();
                    return;
                }
            }
            Items.Add(new OrderTableItem(Id, Description, UnitCost, Quantity, ExtendedCost));
            ((Command)SaveOrderComand).ChangeCanExecute();
        }

        private string CreateOrderString()
        {
            var payload = new Dictionary<string, object>(){
            { "client_id", Client.Id },
            { "clerk_id", Clerk.Id }
            };

            var secretKey = Application.Current.Properties["Secret"] as String;
            string ids = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);


            return JsonConvert.SerializeObject(
           new
           {
               token = ids,
               items = Items
           }
           , Formatting.Indented);
        }
    }
}
