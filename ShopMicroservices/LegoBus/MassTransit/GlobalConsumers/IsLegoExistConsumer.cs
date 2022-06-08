using GlobalContracts.Contracts;
using LegoRepository.RepositoriesMongo.Base;
using MassTransit;

namespace LegoBus.MassTransit.GlobalConsumers
{
    public class IsLegoExistConsumer : IConsumer<IsLegoExistContract>
    {
        private readonly ILegoRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public IsLegoExistConsumer(ILegoRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<IsLegoExistContract> context)
        {
            var result = await _repository.GetByIDAsync(context.Message.LegoId);

            if (result.MessageWhatWrong == null)
            {
                if (context.IsResponseAccepted<IsLegoExistContract>())
                {
                    var data = new IsLegoExistContract() { LegoId = result.Id, IsLegoExist = true };
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<IsLegoExistContract>(data);
                }
            }
            else
            {
                if (context.IsResponseAccepted<IsLegoExistContract>())
                {
                    var data = new IsLegoExistContract() { LegoId = "", IsLegoExist = false };
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<IsLegoExistContract>(data);
                }
            }
        }
    }
}
