using OrderData.Model.Base;
using OrderService.DTOs;

namespace OrderService.Service.Base
{
    public interface IService<TR,TI> where TR : IModel
    {
        Task<TR> MakeOrder(TI orderDTO);
    }
}
