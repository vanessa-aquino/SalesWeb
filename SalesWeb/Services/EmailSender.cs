using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
namespace SalesWeb.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridOptions _options;

        public EmailSender(IOptions<SendGridOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_options.ApiKey);
            var from = new EmailAddress(_options.FromEmail, _options.FromName);
            var to = new EmailAddress(email);
            var plainTextContent = "Clique no link enviado.";
            var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlMessage);

            try
            {
                var response = await client.SendEmailAsync(message);
                var responseBody = await response.Body.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

public class SendGridOptions
{
    public required string ApiKey { get; set; }
    public required string FromEmail { get; set; }
    public required string FromName { get; set; }

}