using GoldenLeafMobile.Models;
using GoldenLeafMobile.Service;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels
{
    public class ListViewModel<T> : BaseViewModel where T : class
    {
        public string URL { get; set; }
        public readonly string FAIL = "OnFetchingEntities";
        public readonly string SELECTED = "OnEntitySelected";

        public ObservableCollection<T> Entities { get; set; }

        private T _selectedEntity;
        public T SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
                MessagingCenter.Send(_selectedEntity, SELECTED);
            }
        }

        public ListViewModel()
        {
            Entities = new ObservableCollection<T>();
        }

        public async Task GetEntities()
        {
            Wait = true;
            Entities.Clear();
            using (HttpClient httpClient = new HttpClient())
            {
                var api = new ApiService<T>(httpClient);
                var response = await api.GetEntitiesAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var EntityList = JsonConvert.DeserializeObject<List<T>>(result);

                    foreach (var entity in EntityList)
                    {
                        Entities.Add(entity);
                    }
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
            Wait = false;
        }

    }
}
