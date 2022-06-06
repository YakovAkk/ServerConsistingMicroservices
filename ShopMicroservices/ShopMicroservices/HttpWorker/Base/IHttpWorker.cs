using ShopMicroservices.Enum;
using ShopMicroservices.Model;
using ShopMicroservices.UrlStorage;

namespace ShopMicroservices.httpClient.Base
{
    public interface IHttpWorker
    {
        public string ApiUrl { get; set; }
        Task<ResponceModel> GetAsync(string methodUrl = "");
        Task<ResponceModel> PostAsync(string data, string methodUrl = "");
        Task<ResponceModel> DeleteAsync(string methodUrl = "");
        Task<ResponceModel> UpdateAsync(string data, string methodUrl = "");
    }
}
