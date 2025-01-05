using System.Net;
using System.Net.Mail;

namespace idefny.Services
{
    public interface IEmailService
    {
        Task SendEmail(string receptore, string subject, string body);
    }

    public class EmailService :IEmailService
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task SendEmail(string receptore,string subject,string body)
        {
            var email = configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var password = configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var host = configuration.GetValue<string>("EMAIl_CONFIGURATION:HOST");
            var port = configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

            var smtpClient = new SmtpClient(host,port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(email, password);

            var message = new MailMessage(email!, receptore, subject, body);
            await smtpClient.SendMailAsync(message);

        }



    }
}
