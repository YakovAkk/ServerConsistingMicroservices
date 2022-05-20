using CategoryMicroservice.httpClient.Base;
using CategoryMicroservice.Model;

namespace CategoryMicroservice.HttpWorker
{
    public class HttpWorker : IHttpWorker
    {
        private readonly HttpClient _httpClient;
        public HttpWorker()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ResponceModel> GetAsync(string url)
        {
            var resp = await _httpClient.GetAsync(url);

            var responceModel = new ResponceModel(resp.IsSuccessStatusCode, await resp.Content.ReadAsStringAsync());

            return responceModel;
        }
    }
}
