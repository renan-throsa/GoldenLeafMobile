using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.CategoryViewModels
{
    public class EditViewModel : BaseViewModel
    {
        private readonly string URL_PUT_Category = "https://golden-leaf.herokuapp.com/api/category";
        public ICommand SaveEditedCategoryComand { get; set; }

        public Category Category { get; set; }

        public string Title
        {
            get { return Category.Title; }
            set { Category.Title = value; OnPropertyChanged(); ((Command)SaveEditedCategoryComand).ChangeCanExecute(); }
        }
     
        public EditViewModel(Category _category)
        {
            Category = _category;
            SaveEditedCategoryComand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send(Category, "SavingEditedCategory");
                    },
                      () =>
                      {
                          return !string.IsNullOrEmpty(Category.Title);
                      }

                );
        }

        public async void SaveCategory()
        {
            HttpClient httpClient = new HttpClient();
            var stringContent = new StringContent(Category.ToJson(), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(URL_PUT_Category, stringContent);
            if (response.IsSuccessStatusCode)
            {
                MessagingCenter.Send<Category>(Category, "SuccessPutCategory");
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.Content != null)
                    response.Content.Dispose();

                MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                    "FailedPutCategory");

            }
        }
    }
}
