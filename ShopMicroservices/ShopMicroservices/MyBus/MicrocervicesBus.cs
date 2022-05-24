using MassTransit;
using MicrocerviceContract.Queue;
using ShopMicroservices.MassTransit;
using ShopMicroservices.RabbitMq;

namespace ShopMicroservices.MyBus
{
    public class MicrocervicesBus
    {
        public readonly IBusControl bus;
        public MicrocervicesBus()
        {
            bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                {
                    h.Username(RabbitMqConsts.UserName);
                    h.Password(RabbitMqConsts.Password);
                });
                cfg.ReceiveEndpoint(ConstatsQueue.NotificationQueueNameCategories, ep =>
                {
                    ep.Consumer<UserConsumer>();
                });

            });

            bus.StartAsync();
        }
    }
}
