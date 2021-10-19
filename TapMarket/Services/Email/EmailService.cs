namespace TapMarket.Services
{
    using System;
    using System.Threading.Tasks;
    using SendGrid;
    using SendGrid.Helpers.Mail;

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
            //var apiKey = Environment.GetEnvironmentVariable("TM_API"); 
            var apiKey = "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";

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(senderAddres, senderName);

            var to = new EmailAddress(receiverAddres, receiverName);

            var htmlContent = $"<p>{content}</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);

            var response = await client.SendEmailAsync(msg);
        }
    }
}
