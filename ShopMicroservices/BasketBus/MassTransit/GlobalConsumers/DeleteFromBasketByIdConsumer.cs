using BasketRepository.RepositoriesMongo.Base;
using GlobalContracts.Contracts;
using GlobalContracts.Models;
using MassTransit;

namespace BasketBus.MassTransit.GlobalConsumers
{
    public class DeleteFromBasketByIdConsumer : IConsumer<DeleteFromBasketByIdContract>
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteFromBasketByIdConsumer(IBasketRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<DeleteFromBasketByIdContract> context)
        {
            var responce = new DeleteFromBasketByIdContract();

            if (context.Message.basketIdList != null)
            {
                foreach (var item in context.Message.basketIdList)
                {
                    var data = await _repository.DeleteAsync(item);
                    if(data.MessageWhatWrong == null)
                    {
                        responce.baskets.Add(
                            new OrderModel()
                            {
                                Id = data.Id,
                                User_Id = data.User_Id,
                                Lego_Id = data.Lego_Id,
                                Amount = data.Amount,
                                DateDeal = data.DateDeal
                            }
                        );
                    }
                }
                responce.IsEverythingOk = true;

                if (context.IsResponseAccepted<DeleteFromBasketByIdContract>())
                {
                    await context.RespondAsync<DeleteFromBasketByIdContract>(responce);
                }
                
            }
            else
            {
                var data = new DeleteFromBasketByIdContract()
                {
                    MessageWhatWrong = "Error!!!"
                };
                await context.RespondAsync<DeleteFromBasketByIdContract>(data);
            }
        }
    }
}
