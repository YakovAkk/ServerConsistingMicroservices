using Microsoft.AspNetCore.Mvc;
using ShopMicroservices.httpClient.Base;
using ShopMicroservices.Models.Base;
using ShopMicroservices.UrlStorage;

namespace ShopMicroservices.Controllers.Base
{
    public abstract class MyControllerBase<T>  : ControllerBase where T : IModelDTO
    {
        public abstract IHttpWorker _httpWorker { get; set; }
        public abstract Task<IActionResult> GetAll();
        public abstract Task<IActionResult> GetById([FromRoute] string Id);
        public abstract Task<IActionResult> Create(T model);
        public abstract Task<IActionResult> Update(T model);
        public abstract Task<IActionResult> Delete([FromRoute] string Id);
    }
}
