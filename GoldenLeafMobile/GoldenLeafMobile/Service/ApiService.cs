using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoldenLeafMobile.Service
{
    public class ApiService<T>
    {
        protected readonly HttpClient httpClient;
        protected string baseurl = "https://goldenleafapi.herokuapp.com/api/v1.0/" + typeof(T).Name.ToLower();

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetEntitiesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, baseurl);
            var result = await httpClient.SendAsync(request);
            return result;
        }

        public async Task<HttpResponseMessage> GetEntitiesAsync(int pageNo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, baseurl + $"?PageNo={pageNo}");
            var result = await httpClient.SendAsync(request);
            return result;
        }

        public async Task<HttpResponseMessage> PostEntityAsync(string token, string payload)
        {
            httpClient.DefaultRequestHeaders.Authorization = GetAuth(token);
            var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(baseurl, stringContent);
            return response;
        }

        public async Task<HttpResponseMessage> PutEntityAsync(string token, string payload)
        {
            httpClient.DefaultRequestHeaders.Authorization = GetAuth(token);
            var stringContent = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PutAsync(baseurl, stringContent);
            return response;
        }

        private AuthenticationHeaderValue GetAuth(string token)
        {
            //var encoded = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(token + ":" + ""));
            var auth = new AuthenticationHeaderValue("Bearer", token);
            return auth;
        }
    }
}
