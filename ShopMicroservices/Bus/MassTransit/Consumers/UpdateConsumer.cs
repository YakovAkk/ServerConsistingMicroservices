using Bus.MassTransit.Contracts.ContractsModel;
using CategoryData.Data.Models;
using CategoryRepositories.RepositoriesMongo.Base;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.MassTransit.Consumers
{
    public class UpdateConsumer : IConsumer<CategoryContractUpdate>
    {
        private readonly ICategoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public UpdateConsumer(ICategoryRepository repository, IPublishEndpoint publishEndpoint)
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

        }
    }
}
