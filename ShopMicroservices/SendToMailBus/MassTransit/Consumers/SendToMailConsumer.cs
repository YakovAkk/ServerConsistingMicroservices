using MassTransit;
using SendToMailBus.MassTransit.Contracts;
using SendToMailServices.Model;
using SendToMailServices.Services.Base;

namespace SendToMailBus.MassTransit.Consumers
{
    public class SendToMailConsumer : IConsumer<SendToMailContract>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendToMailService _sendToMailService;
        public SendToMailConsumer(IPublishEndpoint publishEndpoint, ISendToMailService sendToMailService)
        {
            _publishEndpoint = publishEndpoint;
            _sendToMailService = sendToMailService;
        }
        public async Task Consume(ConsumeContext<SendToMailContract> context)
        {
            var request = new MailRequest()
            {
                ToEmail = context.Message.ToEmail,
                Subject = context.Message.Subject,
                Body = context.Message.Body,
                Attachments = context.Message.Attachments
            };

            var responce = await _sendToMailService.SendToMailAsync(request);

            var data = new SendToMailContract()
            {
                ToEmail = context.Message.ToEmail,
                Subject = context.Message.Subject,
                Body = context.Message.Body,
                Attachments = context.Message.Attachments,
                MessageWhatWrong = null
            };

            if (responce != null)
            {
                if (context.IsResponseAccepted<SendToMailContract>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<SendToMailContract>(data);
                }
            }
            else
            {
                var userResponce = new SendToMailContract()
                {
                    MessageWhatWrong = "Incorrect creditals"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
