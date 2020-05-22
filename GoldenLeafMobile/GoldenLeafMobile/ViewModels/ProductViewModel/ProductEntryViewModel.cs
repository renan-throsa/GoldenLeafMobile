using GoldenLeafMobile.Data;
using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.Models.ProductModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ProductViewModels
{
    public class ProductEntryViewModel : BaseViewModel
    {
        private readonly string URL_POST_PRODUCT = "https://golden-leaf.herokuapp.com/api/product";
        private readonly string URL_GET_CATEGORY = "https://golden-leaf.herokuapp.com/api/category";

        public ICommand SaveProductComand { get; set; }       

        public Product Product { get; private set; }
        public List<Category> Categories { get; private set; }

        public int CategoryId
        {
            get { return Product.CategoryId; }
            set { Product.CategoryId = value; ((Command)SaveProductComand).ChangeCanExecute(); }
        }

        public string Code
        {
            get { return Product.Code; }
            set { Product.Code = value; ((Command)SaveProductComand).ChangeCanExecute(); }
        }
        public string Description
        {
            get { return Product.Description; }
            set { Product.Description = value; ((Command)SaveProductComand).ChangeCanExecute(); }
        }
        public bool IsAvailable
        {
            get { return Product.IsAvailable; }
            set { Product.IsAvailable = value; ((Command)SaveProductComand).ChangeCanExecute(); }
        }


        public float UnitCost
        {
            get { return Product.UnitCost; }
            set { Product.UnitCost = value; ((Command)SaveProductComand).ChangeCanExecute(); }
        }



        public ProductEntryViewModel()
        {
            Product = new Product();            
            SaveProductComand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Product>(Product, "SavingClient");

                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(Product.Description)
                        && !string.IsNullOrEmpty(Product.Code);

                    }
                );
        }

        public async void SaveProduct()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var stringContent = new StringContent(Product.ToJson(), Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync(URL_POST_PRODUCT, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    Product.Syncronized = true;
                    MessagingCenter.Send<Product>(Product, "SuccessPostProduct");
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                        response.Content.Dispose();

                    Product.Syncronized = false;
                    MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                        "FailedPostProduct");

                }

            }

            SaveProductInternaly();

        }

        private void SaveProductInternaly()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var dao = new Repository<Product>(connection);
                dao.Save(this.Product);
            }

        }

        public async Task GetCategories()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(URL_GET_CATEGORY);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var ClientsList = JsonConvert.DeserializeObject<List<Category>>(result);
                    Categories = ClientsList;
                }
                else
                {
                    using (var connection = DependencyService.Get<ISQLite>().GetConnection())
                    {
                        var dao = new Repository<Category>(connection);
                        Categories = dao.Get();
                    }
                }


            }
        }
    }
}
