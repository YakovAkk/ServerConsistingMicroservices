using LegoBus.MassTransit.Contracts;
using LegoData.Data.Models;
using LegoRepository.RepositoriesMongo.Base;
using MassTransit;

namespace LegoBus.MassTransit.Consumers
{
    public class DeleteLegoConsumer : IConsumer<LegoContractDelete>
    {
        private readonly ILegoRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteLegoConsumer(ILegoRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<LegoContractDelete> context)
        {
            var data = await _repository.DeleteAsync(context.Message.Id);

            if (data != null)
            {
                if (context.IsResponseAccepted<LegoContractDelete>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<LegoContractDelete>(data);
                }
            }
            else
            {
                var userResponce = new LegoContractDelete()
                { 
                    MessageWhatWrong = "Incorrect creditals"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
