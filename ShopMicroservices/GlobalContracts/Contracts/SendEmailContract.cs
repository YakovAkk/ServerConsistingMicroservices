using Microsoft.AspNetCore.Http;

namespace GlobalContracts.Contracts
{
    public class SendEmailContract
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MessageWhatWrong { get; set; }

        public SendEmailContract()
        {
           
        }
    }
}
