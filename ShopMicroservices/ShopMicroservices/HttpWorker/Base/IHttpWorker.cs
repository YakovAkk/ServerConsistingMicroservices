using ShopMicroservices.Model;

namespace ShopMicroservices.httpClient.Base
{
    public interface IHttpWorker
    {
        Task<ResponceModel> GetAsync(string url);
        Task<ResponceModel> PostAsync(string url , string data);
        Task<ResponceModel> DeleteAsync(string url);
        Task<ResponceModel> UpdateAsync(string url, string data);
    }
}
