using AccountBus.MassTransit.Contracts;
using AccountData.Models;
using AccountRepository.RepositorySql.Base;
using MassTransit;

namespace AccountBus.MassTransit.Consumers
{
    public class LoginConsumer : IConsumer<AccountContractLogin>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public LoginConsumer(IAccountRepository accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<AccountContractLogin> context)
        {
            if (context.Message == null || string.IsNullOrEmpty(context.Message.Email) || string.IsNullOrEmpty(context.Message.Password))
            {
                var userResponce = new AccountContractLogin()
                {
                    MessageThatWrong = "Password or email is empty"
                };
                await _publishEndpoint.Publish(userResponce);
            }
            else
            {
                var user = new UserModel()
                {
                    Name = context.Message.Email,
                    Email = context.Message.Email,
                    Password = context.Message.Password,
                    RememberMe = context.Message.RememberMe
                };

                var result = await _accountRepository.LoginAsync(user);

                if (result != null)
                {
                    if (context.IsResponseAccepted<AccountContractLogin>())
                    {
                        await _publishEndpoint.Publish(result);
                        await context.RespondAsync<AccountContractLogin>(result);
                    }
                }
                else
                {
                    var userResponce = new AccountContractLogin()
                    {
                        MessageThatWrong = "Incorrect creditals"
                    };
                    await _publishEndpoint.Publish(userResponce);
                }
            }
           
            
        }
    }
}
