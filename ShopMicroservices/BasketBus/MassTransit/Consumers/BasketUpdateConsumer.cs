using BasketBus.MassTransit.Contracts;
using BasketData.Data.Base.Models;
using BasketRepository.RepositoriesMongo.Base;
using GlobalContracts.Contracts;
using MassTransit;

namespace BasketBus.MassTransit.Consumers
{
    public class BasketUpdateConsumer : IConsumer<BasketContractUpdate>
    {
        private readonly IRequestClient<IsLegoExistContract> _isLegoExistClient;
        private readonly IRequestClient<IsUserExistContract> _isUserExistClient;
        private readonly IBasketRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public BasketUpdateConsumer(IBasketRepository repository, 
             IPublishEndpoint publishEndpoint,
             IRequestClient<IsLegoExistContract> isLegoExistClient,
             IRequestClient<IsUserExistContract> isUserExistClient)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
            _isLegoExistClient = isLegoExistClient;
            _isUserExistClient = isUserExistClient;
        }

        public async Task Consume(ConsumeContext<BasketContractUpdate> context)
        {
            var basket = new BasketModel()
            {
                Id = context.Message.Id,
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
            else
            {
                var resBasket = new BasketModel();
                resBasket.MessageWhatWrong = "Lego Or User don't exist";
                await _publishEndpoint.Publish(resBasket);
            }
        }
    }
}
