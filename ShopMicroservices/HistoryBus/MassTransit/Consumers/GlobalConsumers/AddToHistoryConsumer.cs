using GlobalContracts.Contracts;
using HistoryData.Data.Models;
using HistoryRepository.RepositoriesMongo.Base;
using MassTransit;

namespace HistoryBus.MassTransit.Consumers.GlobalConsumers
{
    public class AddToHistoryConsumer : IConsumer<AddToHistoryContract>
    {
        private readonly IHistoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public AddToHistoryConsumer(IHistoryRepository repository,
            IPublishEndpoint publishEndpoint
            )
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<AddToHistoryContract> context)
        {
            var historyModel = new HistoryModel()
            {
                User_Id = context.Message.User_Id,
                Orders = context.Message.Orders
            };
            var data = await _repository.AddAsync(historyModel);

            if(data.MessageWhatWrong == null)
            {
                if (context.IsResponseAccepted<AddToHistoryContract>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<AddToHistoryContract>(data);
                }
            }
            else
            {
                var userResponce = new AddToHistoryContract()
                {
                    MessageWhatWrong = "Error!"
                };
                await context.RespondAsync<AddToHistoryContract>(data);
            }
        }
    }
}
