using GoldenLeafMobile.Models.CategoryModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.CategoryViewModels
{
    public class ListViewModel: BaseViewModel
    {
        private const string URL_GET_CATEGORIES = "https://golden-leaf.herokuapp.com/api/category";

        public ObservableCollection<Category> Categories { get; set; }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                MessagingCenter.Send(_selectedCategory, "SelectedCategory");
            }
        }


        public ListViewModel()
        {
            Categories = new ObservableCollection<Category>();
        }

        public async Task GetCategories()
        {
            Wait = true;
            Categories.Clear();
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.GetStringAsync(URL_GET_CATEGORIES);

                var CategoriesList = JsonConvert.DeserializeObject<List<Category>>(result);

                foreach (var clientJson in CategoriesList)
                {
                    Categories.Add(clientJson);
                }

            }
            Wait = false;
        }
    }
}
