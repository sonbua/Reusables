using System.Threading.Tasks;
using MailingServiceDemo.Model;
using Reusables.Diagnostics.Logging;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class Firewall : ISmtpClientWrapper
    {
        private readonly ILogger _logger;
        private readonly ISmtpClientWrapper _smtpClient;

        public Firewall(ILogger logger, ISmtpClientWrapper smtpClient)
        {
            _logger = logger;
            _smtpClient = smtpClient;
        }

        public void Send(MailMessage message)
        {
            _logger.Info("Delivering....................");

            _smtpClient.Send(message);
        }

        public async Task SendAsync(MailMessage message)
        {
            _logger.Info("Delivering....................");

            await _smtpClient.SendAsync(message);
        }
    }
}