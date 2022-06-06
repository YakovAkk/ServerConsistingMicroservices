using BasketBus.MassTransit.Contracts;
using BasketData.Data.Base.Models;
using BasketRepository.RepositoriesMongo.Base;
using BasketRepository.RepositorySql.Base;
using MassTransit;

namespace BasketBus.MassTransit.Consumers
{
    public class BasketCreateConsumer : IConsumer<BasketContractCreate>
    {
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUserRepository _userService;
        public BasketCreateConsumer(IBasketRepository repository, IPublishEndpoint publishEndpoint, IUserRepository userService)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<BasketContractCreate> context)
        {
            var resUser = await _userService.FindByEmailAsync(context.Message.User.Email);

            if (resUser.MessageThatWrong != null && resUser.MessageThatWrong.Trim() != "")
            {
                resUser = new BasketData.Data.Models.UserModel()
                {
                    MessageThatWrong = "Database doesn't contain the user"
                };

                var data = new BasketContractCreate()
                {
                    MessageWhatWrong = resUser.MessageThatWrong
                };

                await _publishEndpoint.Publish(data);
                await context.RespondAsync<BasketContractCreate>(data);
            }
            else
            {
                var basket = new BasketModel()
                {
                    Amount = Convert.ToUInt32(context.Message.Amount),
                    Lego = context.Message.Lego,
                    User = resUser
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
}
