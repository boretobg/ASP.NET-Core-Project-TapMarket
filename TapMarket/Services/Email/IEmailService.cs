namespace TapMarket.Services
{
    public interface IEmailService
    {
        public void SendEmail(
           string senderName,
           string senderAddres,
           string receiverName,
           string receiverAddres,
           string subject,
           string content);
    }
}
