using Microsoft.AspNetCore.Mvc;
using ShopMicroservices.httpClient.Base;
using ShopMicroservices.Models.Base;
using ShopMicroservices.UrlStorage;

namespace ShopMicroservices.Controllers.Base
{
    public abstract class MyControllerBase<T>  : ControllerBase where T : IModel
    {
        protected readonly IHttpWorker _httpWorker;
        protected readonly MyUrlStorage _urlStorage;
        public MyControllerBase(IHttpWorker httpWorker)
        {
            _httpWorker = httpWorker;
            _urlStorage = MyUrlStorage.getInstance();
        }

        public abstract Task<IActionResult> GetAll();
        public abstract Task<IActionResult> GetById([FromQuery] string Id);
        public abstract Task<IActionResult> Create(T model);
        public abstract Task<IActionResult> Update(T model);
        public abstract Task<IActionResult> Delete([FromQuery] string Id);
    }
}
