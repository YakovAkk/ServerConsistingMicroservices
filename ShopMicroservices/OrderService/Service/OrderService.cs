using MassTransit;
using OrderBus.Contracts;
using OrderData.Model;
using OrderService.DTOs;
using OrderService.Service.Base;

namespace OrderService.Service
{
    public class OrderService : BaseService<OrderModel, OrderDTO>, IOrderService
    {
        private readonly IRequestClient<OrderContract> _makeOrderClient;
        public OrderService(IRequestClient<OrderContract> makeOrderClient)
        {
            _makeOrderClient = makeOrderClient;
        }

        public override async Task<OrderModel> MakeOrder(OrderDTO orderDTO)
        {
            var result = await _makeOrderClient.GetResponse<OrderContract>(orderDTO);

            if(result.Message.MessageWhatWrong != null && result.Message.IsOrderCompleted)
            {
                return new OrderModel()
                {
                    Id = result.Message.Id,
                    User_Id = result.Message.User_Id,
                    BasketIds = result.Message.BasketIds,
                    IsOrderCompleted = result.Message.IsOrderCompleted,
                };
            }

            return new OrderModel()
            {
                MessageWhatWrong = "the order isn't success"
            };
        }
    }
}
