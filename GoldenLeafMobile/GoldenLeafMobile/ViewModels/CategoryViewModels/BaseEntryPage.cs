using GoldenLeafMobile.Data;
using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Service;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.CategoryViewModels
{
    public abstract class BaseEntryPage
    {
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
            if (!this.Clerk.IsTokenValid())
            {
                MessagingCenter.Send<string>(Clerk.UserName, ACCESS);
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var api = new ApiService<Category>(httpClient);
                HttpResponseMessage response;
                if (Category.Id == 0)
                {
                    response = await api.PostEntityAsync(Clerk.GetToken(), Category.ToJson());
                }
                else
                {
                    response = await api.PutEntityAsync(Clerk.GetToken(), Category.ToJson());
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
