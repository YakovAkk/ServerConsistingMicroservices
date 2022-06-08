using AccountData.Models;
using AccountRepository.RepositorySql.Base;
using GlobalContracts.Contracts;
using MassTransit;

namespace AccountBus.MassTransit.GlobalConsumers
{
    public class IsUserExistConsumer : IConsumer<IsUserExistContract>
    {
        private readonly IAccountRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public IsUserExistConsumer(IAccountRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<IsUserExistContract> context)
        {
            var user = new UserModel()
            {
                Id = context.Message.UserId
            };
            var result = await _repository.isDataBaseHasUser(user);

            if (result)
            {
                if (context.IsResponseAccepted<IsUserExistContract>())
                {
                    var data = new IsUserExistContract() { UserId = context.Message.UserId, IsUserExist = true };
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<IsUserExistContract>(data);
                }
            }
            else
            {
                if (context.IsResponseAccepted<IsUserExistContract>())
                {
                    var data = new IsUserExistContract() { UserId = "", IsUserExist = false };
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<IsUserExistContract>(data);
                }
            }
        }
    }
}
