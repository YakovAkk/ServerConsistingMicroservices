using HistoryBus.MassTransit.Contracts;
using HistoryRepository.RepositoriesMongo.Base;
using MassTransit;

namespace HistoryBus.MassTransit.Consumers.LocalConsumers
{
    public class DeleteHistoryConsumer : IConsumer<HistoryContractDelete>
    {
        private readonly IHistoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteHistoryConsumer(IHistoryRepository repository,
            IPublishEndpoint publishEndpoint
           )
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<HistoryContractDelete> context)
        {
            var data = await _repository.DeleteAsync(context.Message.Id);

            if (data != null)
            {
                if (context.IsResponseAccepted<HistoryContractDelete>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<HistoryContractDelete>(data);
                }
            }
            else
            {
                var userResponce = new HistoryContractDelete()
                {
                    MessageWhatWrong = "Incorrect creditals"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
