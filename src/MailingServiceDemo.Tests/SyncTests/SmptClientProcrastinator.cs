using System.Threading.Tasks;
using MailingServiceDemo.Model;
using Reusables.Diagnostics.Logging;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class SmptClientProcrastinator : ISmtpClientWrapper
    {
        private readonly ILogger _logger;
        private readonly ISmtpClientWrapper _smtpClient;

        public SmptClientProcrastinator(ILogger logger, ISmtpClientWrapper smtpClient)
        {
            _logger = logger;
            _smtpClient = smtpClient;
        }

        public void Send(MailMessage message)
        {
            _smtpClient.Send(message);
        }

        public async Task SendAsync(MailMessage message)
        {
            var delay = Randomizer.Next(100);

            _logger.Info($"Delaying {delay} ms...");

            await Task.Delay(delay);

            await _smtpClient.SendAsync(message);
        }
    }
}