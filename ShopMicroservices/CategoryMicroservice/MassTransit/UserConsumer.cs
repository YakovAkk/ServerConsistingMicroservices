using MassTransit;
using MicrocerviceContract.Interfaces;

namespace CategoryMicroservice.MassTransit
{
    public class UserConsumer : IConsumer<IContract>
    {
        public Task Consume(ConsumeContext<IContract> context)
        {

            return Task.CompletedTask;
        }
    }
}
