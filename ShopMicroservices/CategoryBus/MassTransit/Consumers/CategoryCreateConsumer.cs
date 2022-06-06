using Bus.MassTransit.Contracts.ContractsModel;
using CategoryData.Data.Models;
using CategoryRepositories.RepositoriesMongo.Base;
using MassTransit;

namespace Bus.MassTransit.Consumers
{
    public class CategoryCreateConsumer : IConsumer<CategoryContractCreate>
    {
        private readonly ICategoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CategoryCreateConsumer(ICategoryRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CategoryContractCreate> context)
        {
            var category = new CategoryModel() { ImageUrl = context.Message.ImageUrl, Name = context.Message.Name };
            var data = await _repository.AddAsync(category);

            if (data != null)
            {
                if (context.IsResponseAccepted<CategoryContractCreate>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<CategoryContractCreate>(data);
                }
            }
            else
            {
                var responce = new CategoryContractCreate()
                {
                    MessageWhatWrong = "Database doens't contsin the element"
                };
                await _publishEndpoint.Publish(responce);
            }
        }
    }
}
