using ShopMicroservices.httpClient.Base;
using ShopMicroservices.Model;
using System.Text;

namespace ShopMicroservices.HttpWorker
{
    public class HttpWorker : IHttpWorker
    {
        private readonly HttpClient _httpClient;
        public HttpWorker()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ResponceModel> DeleteAsync(string url)
        {
            var resp = await _httpClient.DeleteAsync(url);

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }

        public async Task<ResponceModel> GetAsync(string url)
        {
            var resp = await _httpClient.GetAsync(url);

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }

        public async Task<ResponceModel> PostAsync(string url, string data)
        {
            var content = new StringContent(data,Encoding.UTF8,"application/json");
            var resp = await _httpClient.PostAsync(url, content);

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }

        public async Task<ResponceModel> UpdateAsync(string url, string data)
        {
            var content = new StringContent(data);
            var resp = await _httpClient.PutAsync(url, content);

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }
    }
}
