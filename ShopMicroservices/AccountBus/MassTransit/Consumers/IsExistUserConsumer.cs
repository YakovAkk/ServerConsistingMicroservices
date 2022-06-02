using AccountBus.MassTransit.Contracts;
using AccountData.Models;
using AccountRepository.RepositorySql.Base;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBus.MassTransit.Consumers
{
    public class IsExistUserConsumer : IConsumer<AccountContractIsExistUser>
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public IsExistUserConsumer(IAccountRepository accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<AccountContractIsExistUser> context)
        {
            var user = new UserModel()
            {
                Name = context.Message.Email,
                Email = context.Message.Email
            };

            var result = await _accountRepository.isDataBaseHasUser(user);

            if (result != null)
            {
                if (context.IsResponseAccepted<AccountContractIsExistUser>())
                {
                    await _publishEndpoint.Publish(result);
                    await context.RespondAsync<AccountContractIsExistUser>(result);
                }
            }
            else
            {
                var data = new AccountContractIsExistUser()
                {
                    MessageThatWrong = "Database doens't contsin the element"
                };
                await _publishEndpoint.Publish(data);
            }

        }
    }
}
