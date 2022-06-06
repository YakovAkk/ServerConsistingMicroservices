using BasketBus.MassTransit.Contracts;
using BasketData.Data.Base.Models;
using BasketRepository.RepositoriesMongo.Base;
using BasketRepository.RepositorySql.Base;
using MassTransit;

namespace BasketBus.MassTransit.Consumers
{
    public class BasketDeleteConsumer : IConsumer<BasketContractDelete>
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUserRepository _userService;
        public BasketDeleteConsumer(IBasketRepository repository, IPublishEndpoint publishEndpoint, IUserRepository userService)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _userService = userService;
        }
        public async Task Consume(ConsumeContext<BasketContractDelete> context)
        {
            var data = await _repository.DeleteAsync(context.Message.Id);

            if (data != null)
            {
                if (context.IsResponseAccepted<BasketContractDelete>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<BasketContractDelete>(data);
                }
            }
            else
            {
                var userResponce = new BasketContractDelete()
                {
                    MessageWhatWrong = "Incorrect creditals"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
