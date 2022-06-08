using BasketBus.MassTransit.Contracts;
using BasketData.Data.Base.Models;
using BasketRepository.RepositoriesMongo.Base;
using GlobalContracts.Contracts;
using MassTransit;

namespace BasketBus.MassTransit.Consumers
{
    public class BasketCreateConsumer : IConsumer<BasketContractCreate>
    {
        private readonly IRequestClient<IsLegoExistContract> _isLegoExistClient;
        private readonly IRequestClient<IsUserExistContract> _isUserExistClient;
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketCreateConsumer(IBasketRepository repository, 
            IPublishEndpoint publishEndpoint, 
            IRequestClient<IsLegoExistContract> isLegoExistClient,
            IRequestClient<IsUserExistContract> isUserExistClient)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _isLegoExistClient = isLegoExistClient;
            _isUserExistClient = isUserExistClient;
        }

        public async Task Consume(ConsumeContext<BasketContractCreate> context)
        {
            var basket = new BasketModel()
            {
                Amount = context.Message.Amount,
                Lego_Id = context.Message.Lego_Id,
                User_Id = context.Message.User_Id
            };

            var legoIdModel = new IsLegoExistContract()
            {
                LegoId = basket.Lego_Id
            };

            var useIdModel = new IsUserExistContract()
            {
                UserId = basket.User_Id
            };

            var isLegoExist = await _isLegoExistClient.GetResponse<IsLegoExistContract>(legoIdModel);
            var isUserExist = await _isUserExistClient.GetResponse<IsUserExistContract>(useIdModel);

            if (isLegoExist.Message.IsLegoExist && isUserExist.Message.IsUserExist)
            {
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
            else
            {
                var resBasket = new BasketContractCreate();
                resBasket.MessageWhatWrong = "Lego Or User don't exist";
                await _publishEndpoint.Publish(resBasket);
            }
            
        }
    }
}
