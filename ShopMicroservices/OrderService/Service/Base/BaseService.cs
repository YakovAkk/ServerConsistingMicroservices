using OrderData.Model.Base;

namespace OrderService.Service.Base
{
    public abstract class BaseService<TR, TI> : IService<TR, TI> where TR : IModel
    {
        public abstract Task<TR> MakeOrder(TI orderDTO);
    }
}
