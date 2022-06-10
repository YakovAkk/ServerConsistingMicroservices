using BasketRepository.RepositoriesMongo.Base;
using GlobalContracts.Contracts;
using MassTransit;

namespace BasketBus.MassTransit.GlobalConsumers
{
    public class GetBasketItemByIdConsumer : IConsumer<BasketItemContract>
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public GetBasketItemByIdConsumer(IBasketRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<BasketItemContract> context)
        {
            if(context.Message.Id != null)
            {
                var data = await _repository.GetByIDAsync(context.Message.Id);
                if(data != null)
                {
                    if (context.IsResponseAccepted<BasketItemContract>())
                    {
                        await context.RespondAsync<BasketItemContract>(data);
                    }
                   
                }
                else
                {
                    var req = new BasketItemContract()
                    {
                        MessageWhatWrong = "Error!!!"
                    };
                    await context.RespondAsync<BasketItemContract>(req);
                }
            }
        }
    }
}
