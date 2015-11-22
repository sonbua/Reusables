using System.Threading.Tasks;
using MailingServiceDemo.Model;

namespace MailingServiceDemo
{
    public interface ISmtpClientWrapper
    {
        void Send(MailMessage message);

        Task SendAsync(MailMessage message);
    }
}
