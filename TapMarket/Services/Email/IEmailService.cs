namespace TapMarket.Services
{
    using System.Threading.Tasks;

    public interface IEmailService
    {
        public Task SendEmail(
           string senderName,
           string senderAddres,
           string receiverName,
           string receiverAddres,
           string subject,
           string content);
    }
}
