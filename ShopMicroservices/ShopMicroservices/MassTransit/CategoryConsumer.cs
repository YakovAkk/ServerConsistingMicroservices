using MassTransit;
using MicrocerviceContract.Contracts.CategoryContracts;

namespace ShopMicroservices.MassTransit
{
    public class CategoryConsumer : IConsumer<CategoryContractCreate>
    {
        public event Action<CategoryContractCreate> CategoryCreateEvent = delegate { };
        public async Task Consume(ConsumeContext<CategoryContractCreate> context)
        {
           CategoryCreateEvent(context);
           await Task.Delay(1);
        }
    }
}
