using System.Threading.Tasks;

namespace TapMarket.Services
{
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
