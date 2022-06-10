using GlobalContracts.Contracts;
using MassTransit;
using SendToMailServices.Model;
using SendToMailServices.Services.Base;

namespace SendToMailBus.MassTransit.Consumers
{
    public class SendToMailConsumer : IConsumer<SendEmailContract>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendToMailService _sendToMailService;
        public SendToMailConsumer(IPublishEndpoint publishEndpoint, ISendToMailService sendToMailService)
        {
            _publishEndpoint = publishEndpoint;
            _sendToMailService = sendToMailService;
        }
        public async Task Consume(ConsumeContext<SendEmailContract> context)
        {
            var request = new MailRequest()
            {
                ToEmail = context.Message.ToEmail,
                Subject = context.Message.Subject,
                Body = context.Message.Body,
            };

            var responce = await _sendToMailService.SendToMailAsync(request);

            var data = new SendEmailContract()
            {
                ToEmail = context.Message.ToEmail,
                Subject = context.Message.Subject,
                Body = context.Message.Body,
                MessageWhatWrong = null
            };

            if (responce != null)
            {
                if (context.IsResponseAccepted<SendEmailContract>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<SendEmailContract>(data);
                }
            }
            else
            {
                var userResponce = new SendEmailContract()
                {
                    MessageWhatWrong = "Incorrect creditals"
                };
                await _publishEndpoint.Publish(userResponce);
            }
        }
    }
}
