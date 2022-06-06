using Microsoft.AspNetCore.Http;

namespace SendToMailBus.MassTransit.Contracts
{
    public class SendToMailContract
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public string MessageWhatWrong { get; set; }
    }
}
