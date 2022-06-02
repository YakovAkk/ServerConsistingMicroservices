using AccountBus.MassTransit.Contracts;
using AccountData.Models;
using AccountRepository.RepositorySql.Base;
using MassTransit;

namespace AccountBus.MassTransit.Consumers
{
    public class RegistrationConsumer : IConsumer<AccountContractRegistration>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public RegistrationConsumer(IAccountRepository accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<AccountContractRegistration> context)
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

            var result = await _accountRepository.CreateAsync(user);

            if(result != null)
            {
                if (context.IsResponseAccepted<AccountContractRegistration>())
                {
                    await _publishEndpoint.Publish(result);
                    await context.RespondAsync<AccountContractRegistration>(result);
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
