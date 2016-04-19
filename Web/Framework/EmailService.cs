namespace forCrowd.Website.Web.Framework
{
    using Models;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;

    public class EmailService
    {
        public bool HasValidConfiguration()
        {
            var hasContactEmailAddress = !string.IsNullOrWhiteSpace(AppSettings.ContactEmailAddress);
            var hasNotificationEmailAddress = !string.IsNullOrWhiteSpace(AppSettings.NotificationEmailAddress);

            var hasSmtpClientConfig = false;

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    hasSmtpClientConfig = !string.IsNullOrWhiteSpace(smtpClient.Host);
                }
            }
            catch
            {
                // Swallow it, it's going to return 'false' as a result, which is enough
            }

            return hasContactEmailAddress &&
                hasNotificationEmailAddress &&
                hasSmtpClientConfig;
        }

        public async Task SendAsync(Message message)
        {
            // Validate
            // 1. Smtp configuration
            if (!HasValidConfiguration())
                return;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(AppSettings.ContactEmailAddress);
            mailMessage.To.Add(new MailAddress(AppSettings.NotificationEmailAddress));
            mailMessage.Subject = "You have a new message";

            var sbBody = new StringBuilder();
            sbBody.AppendLine("    <p>");
            sbBody.AppendFormat("        Name: {0}<br />", message.Name);
            sbBody.AppendFormat("        Email: {0}<br />", message.Email);
            sbBody.AppendFormat("        Subject: {0}<br />", message.Subject);
            sbBody.AppendFormat("        Message: {0}<br />", message.MessageText);
            sbBody.AppendLine("    </p>");

            var body = sbBody.ToString();

            var text = body;
            var html = body;

            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            using (var smtpClient = new SmtpClient())
            {
                // TODO How to use SSL?
                // EnableSSL true doesn't work at the moment, tried all ports (25, 465, 587, 8889) but no luck.
                // api.forcrowd.org SSL cert. also covers mail.forcrowd.org, but does it even necessary, or how to configure it?
                // And/or is it about hosting?
                // coni2k - 08 Feb. '16
                //smtpClient.EnableSsl = AppSettings.EnableSsl;
                smtpClient.EnableSsl = false;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
   }
}