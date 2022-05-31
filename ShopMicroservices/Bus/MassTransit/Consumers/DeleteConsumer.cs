using Bus.MassTransit.Contracts.ContractsModel;
using CategoryRepositories.RepositoriesMongo.Base;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.MassTransit.Consumers
{
    public class DeleteConsumer : IConsumer<CategoryContractDelete>
    {
        private readonly ICategoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteConsumer(ICategoryRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<CategoryContractDelete> context)
        {
            var data = await _repository.GetByIDAsync(context.Message.Id);

            await _repository.DeleteAsync(context.Message.Id);

            if (data != null)
            {
                if (context.IsResponseAccepted<CategoryContractDelete>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<CategoryContractDelete>(data);
                }
            }

        }
    }
}
