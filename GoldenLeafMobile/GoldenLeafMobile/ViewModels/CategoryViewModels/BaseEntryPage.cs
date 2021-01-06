using GoldenLeafMobile.Data;
using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.Models.ClerkModels;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.CategoryViewModels
{
    public abstract class BaseEntryPage
    {
        private readonly string URL_Category = "https://golden-leaf.herokuapp.com/api/category";
        public ICommand SaveCategoryComand { get; set; }

        public Clerk Clerk { get; set; }
        public readonly string ACCESS = "OnRequestUnauthorized";
        public readonly string SUCCESS = "OnSuccessSavingCategory";
        public readonly string FAIL = "OnFailedSavingCategory";
        public readonly string ASK = "OnSavingCategory";

        public Category Category { get; set; }

        public string Title
        {
            get { return Category.Title; }
            set { Category.Title = value; ((Command)SaveCategoryComand).ChangeCanExecute(); }
        }

        public async void SaveCategory()
        {
            if (!this.Clerk.IsTokenExperationTimeValid())
            {
                MessagingCenter.Send<string>(Clerk.Name, ACCESS);
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var stringContent = new StringContent(Category.ToJson(), Encoding.UTF8, "application/json");
                var response = new HttpResponseMessage();
                if (Category.Id == 0)
                {
                    response = await httpClient.PostAsync(URL_Category, stringContent);
                }
                else
                {
                    response = await httpClient.PutAsync(URL_Category, stringContent);
                }

                if (response.IsSuccessStatusCode)
                {
                    Category.Syncronized = true;
                    MessagingCenter.Send<Category>(Category, SUCCESS);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                        response.Content.Dispose();

                    MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                        FAIL);

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
