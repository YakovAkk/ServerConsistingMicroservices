using AccountBus.MassTransit.Contracts;
using AccountData.Models;
using AccountRepository.RepositorySql.Base;
using MassTransit;

namespace AccountBus.MassTransit.Consumers
{
    public class UpdateConsumer : IConsumer<AccountContractUpdate>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateConsumer(IAccountRepository accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<AccountContractUpdate> context)
        {
            var user = new UserModel()
            {
                Id = context.Message.Id,
                Name = context.Message.Name,
                NickName = context.Message.NickName,
                Email = context.Message.Email,
                Password = context.Message.Password,
                RememberMe = context.Message.RememberMe,
                DataRegistration = context.Message.DataRegistration,
                MessageThatWrong = context.Message.MessageThatWrong
            };

            var result = await _accountRepository.UpdateAsync(user);

            if (result != null)
            {
                if (context.IsResponseAccepted<AccountContractUpdate>())
                {
                    await _publishEndpoint.Publish(result);
                    await context.RespondAsync<AccountContractUpdate>(result);
                }
            }
            else
            {
                var data = new AccountContractRegistration()
                {
                    MessageThatWrong = "Database doens't contsin the element"
                };
                await _publishEndpoint.Publish(data);
            }
        }
    }
}
