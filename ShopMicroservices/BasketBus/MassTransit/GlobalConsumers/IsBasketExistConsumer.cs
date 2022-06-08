using BasketRepository.RepositoriesMongo.Base;
using GlobalContracts.Contracts;
using MassTransit;

namespace BasketBus.MassTransit.GlobalConsumers
{
    public class IsBasketExistConsumer : IConsumer<IsBasketExistContract>
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public IsBasketExistConsumer(IBasketRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<IsBasketExistContract> context)
        {
            var result = await _repository.GetByIDAsync(context.Message.BasketId);

            if (result.MessageWhatWrong == null)
            {
                if (context.IsResponseAccepted<IsBasketExistContract>())
                {
                    var data = new IsBasketExistContract() { BasketId = result.Id, IsBasketyExist = true };
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<IsBasketExistContract>(data);
                }
            }
            else
            {
                if (context.IsResponseAccepted<IsBasketExistContract>())
                {
                    var data = new IsBasketExistContract() { BasketId = "", IsBasketyExist = false };
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<IsBasketExistContract>(data);
                }
            }
        }
    }
}

