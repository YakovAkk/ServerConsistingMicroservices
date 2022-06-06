using ShopMicroservices.Enum;
using ShopMicroservices.httpClient.Base;
using ShopMicroservices.Model;
using ShopMicroservices.UrlStorage;
using System.Text;

namespace ShopMicroservices.HttpWorker
{
    public class MyHttpWorker : IHttpWorker
    {
        private readonly HttpClient _httpClient;
        public string ApiUrl { get; set; }
        public MyHttpWorker(UrlEnum urlEnum)
        {
            _httpClient = new HttpClient();
            ApiUrl = MyUrlStorage.getInstance().ApisUrl[urlEnum];
        }
        public async Task<ResponceModel> DeleteAsync(string methodUrl = "")
        {
            var resp = await _httpClient.DeleteAsync($"{ApiUrl}/{methodUrl}");

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }
        public async Task<ResponceModel> GetAsync(string methodUrl = "")
        {
            var resp = await _httpClient.GetAsync($"{ApiUrl}/{methodUrl}");

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }
        public async Task<ResponceModel> PostAsync(string data, string methodUrl = "")
        {
            var content = new StringContent(data,Encoding.UTF8,"application/json");
            var resp = await _httpClient.PostAsync($"{ApiUrl}/{methodUrl}", content);

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }
        public async Task<ResponceModel> UpdateAsync(string data, string methodUrl = "")
        {
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PutAsync($"{ApiUrl}/{methodUrl}", content);

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }
    }
}
