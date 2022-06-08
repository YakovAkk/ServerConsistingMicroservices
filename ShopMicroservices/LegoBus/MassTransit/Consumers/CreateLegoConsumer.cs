using GlobalContracts.Contracts;
using LegoBus.MassTransit.Contracts;
using LegoData.Data.Models;
using LegoRepository.RepositoriesMongo.Base;
using MassTransit;

namespace LegoBus.MassTransit.Consumers
{
    public class CreateLegoConsumer : IConsumer<LegoContractCreate>
    {
        private readonly IRequestClient<IsCategoryExistContract> _isCategoryExistClient;
        private readonly ILegoRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CreateLegoConsumer(ILegoRepository repository, 
            IPublishEndpoint publishEndpoint, 
            IRequestClient<IsCategoryExistContract> isCategoryExistClient)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
            _isCategoryExistClient = isCategoryExistClient;
        }
        public async Task Consume(ConsumeContext<LegoContractCreate> context)
        {
            var lego = new LegoModel()
            {
                Name = context.Message.Name,
                ImageUrl = context.Message.ImageUrl,
                Description = context.Message.Description,
                Price = context.Message.Price,
                isFavorite = context.Message.isFavorite,
                Category_Id = context.Message.Category_Id
            };

            var IsCategoryExistModel = new IsCategoryExistContract() { CategoryId = lego.Category_Id };

            var isCategoryExist = await _isCategoryExistClient.GetResponse<IsCategoryExistContract>(IsCategoryExistModel);

            if (isCategoryExist.Message.IsCategoryExist)
            {
                var data = await _repository.AddAsync(lego);
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
            else
            {
                var userResponce = new LegoContractCreate()
                {
                    MessageWhatWrong = "The Category doesn't exist"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
