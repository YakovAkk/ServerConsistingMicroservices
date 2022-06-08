using CategoryRepositories.RepositoriesMongo.Base;
using GlobalContracts.Contracts;
using MassTransit;

namespace CategoryBus.MassTransit.Consumers.GlobalConsumers
{
    public class IsCategoryExistConsumer : IConsumer<IsCategoryExistContract>
    {
        private readonly ICategoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public IsCategoryExistConsumer(ICategoryRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<IsCategoryExistContract> context)
        {
            var result = await _repository.GetByIDAsync(context.Message.CategoryId);

            if(result.MessageWhatWrong == null)
            {
                if (context.IsResponseAccepted<IsCategoryExistContract>())
                {
                    var data = new IsCategoryExistContract() { CategoryId = result.Id, IsCategoryExist = true };
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<IsCategoryExistContract>(data);
                }
            }
            else
            {
                if (context.IsResponseAccepted<IsCategoryExistContract>())
                {
                    var data = new IsCategoryExistContract() { CategoryId = "", IsCategoryExist = false };
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<IsCategoryExistContract>(data);
                }
            }
        }
    }
}
