using BasketBus.MassTransit.Contracts;
using BasketData.Data.Base.Models;
using BasketRepository.RepositoriesMongo.Base;
using MassTransit;

namespace BasketBus.MassTransit.Consumers
{
    public class BasketUpdateConsumer : IConsumer<BasketContractUpdate>
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public BasketUpdateConsumer(IBasketRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<BasketContractUpdate> context)
        {
            var basket = new BasketModel()
            {
                Amount = context.Message.Amount,
                Lego_Id = context.Message.Lego_Id,
                User_Id = context.Message.User_Id
            };

            var data = await _repository.UpdateAsync(basket);

            if (data != null)
            {
                if (context.IsResponseAccepted<BasketContractUpdate>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<BasketContractUpdate>(data);
                }
            }
            else
            {
                var userResponce = new BasketContractUpdate()
                {
                    MessageWhatWrong = "Incorrect creditals"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
