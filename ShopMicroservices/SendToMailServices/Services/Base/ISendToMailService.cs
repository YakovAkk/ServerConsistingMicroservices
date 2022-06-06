using SendToMailServices.Model;

namespace SendToMailServices.Services.Base
{
    public interface ISendToMailService
    {
        Task<string> SendToMailAsync(MailRequest mailRequest);
    }
}
