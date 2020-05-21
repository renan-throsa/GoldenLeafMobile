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
        private string URL = "https://golden-leaf.herokuapp.com/api/" + typeof(T).Name.ToLower();

        public ObservableCollection<T> Entities { get; set; }


        private T _selectedEntity;
        public T SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
                MessagingCenter.Send(_selectedEntity, "Selected" + typeof(T).Name);
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
                var result = await httpClient.GetStringAsync(URL);

                var ClientsList = JsonConvert.DeserializeObject<List<T>>(result);

                foreach (var clientJson in ClientsList)
                {
                    Entities.Add(clientJson);
                }

            }
            Wait = false;
        }

    }
}
