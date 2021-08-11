namespace TapMarket.Services
{
    using MailKit.Net.Smtp;
    using MimeKit;

    public class EmailService : IEmailService
    {
       public void SendEmail(
           string senderName, 
           string senderAddres,
           string receiverName, 
           string receiverAddres,
           string subject, 
           string content)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(senderName, senderAddres);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(receiverName, receiverAddres);
            message.To.Add(to);

            message.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = content;

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp_gmail.com", 587, false);
            client.Authenticate("user_name_here", "pwd_here"); //same as from

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
