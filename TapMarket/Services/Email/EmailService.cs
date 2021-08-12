namespace TapMarket.Services
{
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using System;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
       public async Task SendEmail(
           string senderName, 
           string senderAddres,
           string receiverName, 
           string receiverAddres,
           string subject, 
           string content)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(senderAddres, senderName);

           // var subject = "Sending with SendGrid is Fun";

            var to = new EmailAddress(receiverAddres, receiverName);

            var htmlContent = $"<p>{content}</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
    }
}
