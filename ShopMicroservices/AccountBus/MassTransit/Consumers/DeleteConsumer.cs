using AccountBus.MassTransit.Contracts;
using AccountRepository.RepositorySql.Base;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBus.MassTransit.Consumers
{
    public class DeleteConsumer : IConsumer<AccountContractDelete>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public DeleteConsumer(IAccountRepository accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<AccountContractDelete> context)
        {
            var result = await _accountRepository.DeleteAsync(context.Message.Id);

            if(result != null)
            {
                if (context.IsResponseAccepted<AccountContractDelete>())
                {
                    await _publishEndpoint.Publish(result);
                    await context.RespondAsync<AccountContractDelete>(result);
                }
            }
            else
            {
                var data = new AccountContractDelete()
                {
                    MessageThatWrong = "Database doens't contsin the element"
                };
                await _publishEndpoint.Publish(data);
            }
            
        }
    }
}
