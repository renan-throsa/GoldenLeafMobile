using GoldenLeafMobile.Data;
using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.CategoryViewModels
{
    public class CategoryEntryViewModel : BaseViewModel
    {
        private readonly string URL_POST_CLIENT = "https://golden-leaf.herokuapp.com/api/category";
        public ICommand SaveCategoryComand { get; set; }

        public Category Category { get; private set; }

        public string Title
        {
            get { return Category.Title; }
            set { Category.Title = value;((Command)SaveCategoryComand).ChangeCanExecute(); }
        }
       

        public CategoryEntryViewModel()
        {
            Category = new Category();
            SaveCategoryComand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Category>(Category, "SavingCategory");

                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(Category.Title);
                    }
                );
        }

        public async void SaveCategory()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var stringContent = new StringContent(Category.ToJson(), Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync(URL_POST_CLIENT, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    Category.Syncronized = true;
                    MessagingCenter.Send<Category>(Category, "SuccessPostCategory");
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                        response.Content.Dispose();

                    Category.Syncronized = false;
                    MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                        "FailedPostClient");

                }

            }

            SaveCategoryInternaly();

        }

        private void SaveCategoryInternaly()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var dao = new Repository<Category>(connection);
                dao.Save(this.Category);
            }

        }
    }
}
