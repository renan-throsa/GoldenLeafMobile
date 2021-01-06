using GoldenLeafMobile.Data;
using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ProductModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ProductViewModel
{
    public abstract class BaseProductViewModel : BaseViewModel
    {
        private readonly string URL_PRODUCT = "https://golden-leaf.herokuapp.com/api/product";
        private readonly string URL_CATEGORY = "https://golden-leaf.herokuapp.com/api/category";
        public readonly string SUCCESS = "SuccessSavingProduct";
        public readonly string FAIL = "FailedSavingProduct";
        public readonly string ASK = "SavingProduct";

        public Clerk Clerk { get; set; }
        public readonly string ACCESS = "RequestUnauthorized";


        public ICommand SaveProductCommand { get; set; }
        protected Product Product { get; set; }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; Product.CategoryId = value.Id; }
        }

        public int CategoryId
        {
            get { return Product.CategoryId; }
            set { Product.CategoryId = value; ((Command)SaveProductCommand).ChangeCanExecute(); }
        }

        public string Description
        {
            get { return Product.Description; }
            set { Product.Description = value; ((Command)SaveProductCommand).ChangeCanExecute(); }
        }
        public bool IsAvailable
        {
            get { return Product.IsAvailable; }
            set { Product.IsAvailable = value; ((Command)SaveProductCommand).ChangeCanExecute(); }
        }

        public string Code
        {
            get { return Product.Code; }
            set { Product.Code = value; ((Command)SaveProductCommand).ChangeCanExecute(); }
        }

        public float UnitCost
        {
            get { return Product.UnitCost; }
            set { Product.UnitCost = value; ((Command)SaveProductCommand).ChangeCanExecute(); }
        }

        public ObservableCollection<Category> Categories { get; private set; }

        public BaseProductViewModel(Product product)
        {
            Product = product;
            Categories = new ObservableCollection<Category>();
            MessagingCenter.Subscribe<Clerk>(this, "CurrentClerk", (_clerk) =>
            {
                this.Clerk = _clerk;
            });
        }

        public async void SaveProduct()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var encoded = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(Clerk.GetToken() + ":" + ""));
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {encoded}");
                var stringContent = new StringContent(Product.ToJson(), Encoding.UTF8, "application/json");

                var response = new HttpResponseMessage();
                if (Product.Id == 0)
                {
                    response = await httpClient.PostAsync(URL_PRODUCT, stringContent);
                }
                else
                {
                    response = await httpClient.PutAsync(URL_PRODUCT, stringContent);
                }

                if (response.IsSuccessStatusCode)
                {
                    Product.Syncronized = true;
                    MessagingCenter.Send<Product>(Product, SUCCESS);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                        response.Content.Dispose();

                    Product.Syncronized = false;
                    var simpleHttpResponse = new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content);
                    MessagingCenter.Send(simpleHttpResponse, FAIL);

                }

            }

            SaveProductInternaly();

        }

        public async Task GetCategories()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(URL_CATEGORY);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var EtitiesList = JsonConvert.DeserializeObject<List<Category>>(result);
                    foreach (var clientJson in EtitiesList)
                    {
                        Categories.Add(clientJson);
                    }
                }
                else
                {
                    using (var connection = DependencyService.Get<ISQLite>().GetConnection())
                    {
                        var dao = new Repository<Category>(connection);
                        foreach (var category in dao.Get())
                        {
                            Categories.Add(category);
                        }

                    }
                }

            }
        }

        private void SaveProductInternaly()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var dao = new Repository<Product>(connection);
                dao.Save(this.Product);
            }

        }
    }
}
