using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using MailingServiceDemo.Model;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class FailureSmtpClientWrapper : ISmtpClientWrapper
    {
        private readonly Exception[] _exceptions =
        {
            new NetworkInformationException(1),
            new NetworkInformationException(2),
            new NetworkInformationException(3),
            new NetworkInformationException(4),
            new NetworkInformationException(5),
            new NetworkInformationException(6),
            new NetworkInformationException(7),
            new NetworkInformationException(8),
            new NetworkInformationException(9),
            new EntryPointNotFoundException("could not connect to email service"),
        };

        public void Send(MailMessage message)
        {
            throw _exceptions[Randomizer.Next(_exceptions.Length - 1)];
        }

        public Task SendAsync(MailMessage message)
        {
            throw _exceptions[Randomizer.Next(_exceptions.Length - 1)];
        }
    }
}