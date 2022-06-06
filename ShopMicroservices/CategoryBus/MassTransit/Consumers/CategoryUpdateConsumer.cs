using Bus.MassTransit.Contracts.ContractsModel;
using CategoryData.Data.Models;
using CategoryRepositories.RepositoriesMongo.Base;
using MassTransit;

namespace Bus.MassTransit.Consumers
{
    public class CategoryUpdateConsumer : IConsumer<CategoryContractUpdate>
    {
        private readonly ICategoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CategoryUpdateConsumer(ICategoryRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<CategoryContractUpdate> context)
        {
            var category = new CategoryModel()
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                ImageUrl = context.Message.ImageUrl
            };

            var data = await _repository.UpdateAsync(category);

            if (data != null)
            {
                if (context.IsResponseAccepted<CategoryContractUpdate>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<CategoryContractUpdate>(data);
                }
            }
            else
            {
                var responce = new CategoryContractUpdate()
                {
                    MessageWhatWrong = "Database doens't contsin the element"
                };
                await _publishEndpoint.Publish(responce);
            }

        }
    }
}
