using System.Net.Mail;

namespace MailingServiceDemo
{
    public interface ISmtpClientWrapper
    {
        void Send(MailMessage message);
    }
}