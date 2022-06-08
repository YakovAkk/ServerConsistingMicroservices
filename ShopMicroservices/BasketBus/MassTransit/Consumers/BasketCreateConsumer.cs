using BasketBus.MassTransit.Contracts;
using BasketData.Data.Base.Models;
using BasketRepository.RepositoriesMongo.Base;
using MassTransit;

namespace BasketBus.MassTransit.Consumers
{
    public class BasketCreateConsumer : IConsumer<BasketContractCreate>
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketCreateConsumer(IBasketRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<BasketContractCreate> context)
        {
            var basket = new BasketModel()
            {
                Amount = context.Message.Amount,
                Lego_Id = context.Message.Lego_Id,
                User_Id = context.Message.User_Id
            };

            var data = await _repository.AddAsync(basket);

            if (data != null)
            {
                if (context.IsResponseAccepted<BasketContractCreate>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<BasketContractCreate>(data);
                }
            }
            else
            {
                var userResponce = new BasketContractCreate()
                {
                    MessageWhatWrong = "Incorrect creditals"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
