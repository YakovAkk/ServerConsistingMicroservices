using LegoBus.MassTransit.Contracts;
using LegoData.Data.Models;
using LegoRepository.RepositoriesMongo.Base;
using MassTransit;

namespace LegoBus.MassTransit.Consumers
{
    public class UpdateLegoConsumer : IConsumer<LegoContractUpdate>
    {
        private readonly ILegoRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public UpdateLegoConsumer(ILegoRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<LegoContractUpdate> context)
        {
            var lego = new LegoModel()
            {
                Name = context.Message.Name,
                ImageUrl = context.Message.ImageUrl,
                Description = context.Message.Description,
                Price = context.Message.Price,
                isFavorite = context.Message.isFavorite,
                Category = context.Message.Category
            };

            var data = await _repository.UpdateAsync(lego);

            if (data != null)
            {
                if (context.IsResponseAccepted<LegoContractCreate>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<LegoContractCreate>(data);
                }
            }
            else
            {
                var userResponce = new LegoContractCreate()
                {
                    MessageWhatWrong = "Incorrect creditals" 
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
