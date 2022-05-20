using CategoryMicroservice.Model;

namespace CategoryMicroservice.httpClient.Base
{
    public interface IHttpWorker
    {
        Task<ResponceModel> GetAsync(string url);
    }
}
