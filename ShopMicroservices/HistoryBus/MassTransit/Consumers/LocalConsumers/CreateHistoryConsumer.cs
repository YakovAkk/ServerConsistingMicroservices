using GlobalContracts.Contracts;
using HistoryBus.MassTransit.Contracts;
using HistoryData.Data.Models;
using HistoryRepository.RepositoriesMongo.Base;
using MassTransit;

namespace HistoryBus.MassTransit.Consumers.LocalConsumers
{
    public class CreateHistoryConsumer : IConsumer<HistoryContractCreate>
    {
        private readonly IRequestClient<IsBasketExistContract> _isBasketExistClient;
        private readonly IRequestClient<IsUserExistContract> _isUserExistClient;
        private readonly IHistoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CreateHistoryConsumer(IHistoryRepository repository,
            IPublishEndpoint publishEndpoint,
            IRequestClient<IsBasketExistContract> isBasketExistClient,
            IRequestClient<IsUserExistContract> isUserExistClient)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
            _isBasketExistClient = isBasketExistClient;
            _isUserExistClient = isUserExistClient;
        }

        public async Task Consume(ConsumeContext<HistoryContractCreate> context)
        {
            var history = new HistoryModel()
            {
                User_Id = context.Message.User_Id,
                Orders_Id = context.Message.Orders_Id
            };

            var IsUserExistModel = new IsUserExistContract() { UserId = history.User_Id };
            var IsUserExist = await _isUserExistClient.GetResponse<IsUserExistContract>(IsUserExistModel);

            bool IsAllOk = true;

            foreach (var basketId in history.Orders_Id)
            {
                var IsBasketExistModel= new IsBasketExistContract() { BasketId = basketId};

                var isBasketExist = await _isBasketExistClient.GetResponse<IsBasketExistContract>(IsBasketExistModel);

                if (!isBasketExist.Message.IsBasketyExist)
                {
                    IsAllOk = false;
                    break;
                }
            }

            if (IsAllOk && IsUserExist.Message.IsUserExist)
            {
                var data = await _repository.AddAsync(history);
                if (data != null)
                {
                    if (context.IsResponseAccepted<HistoryContractCreate>())
                    {
                        await _publishEndpoint.Publish(data);
                        await context.RespondAsync<HistoryContractCreate>(data);
                    }
                }
                else
                {
                    var userResponce = new HistoryContractCreate()
                    {
                        MessageWhatWrong = "Incorrect creditals"
                    };
                    await _publishEndpoint.Publish(userResponce);
                }
            }
            else
            {
                var userResponce = new HistoryContractCreate()
                {
                    MessageWhatWrong = "The Basket or User doesn't exist"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
