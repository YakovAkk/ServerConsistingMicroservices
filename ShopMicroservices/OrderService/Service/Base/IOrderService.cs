using OrderData.Model;
using OrderService.DTOs;

namespace OrderService.Service.Base
{
    public interface IOrderService : IService<OrderModel, OrderDTO>
    {

    }
}
