using System.Threading.Tasks;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class SuccessSmtpClientWrapper : ISmtpClientWrapper
    {
        public void Send(MailMessage message)
        {
        }

        public async Task SendAsync(MailMessage message)
        {
            await Task.Yield();
        }
    }
}