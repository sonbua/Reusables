using System.Threading;
using Xunit.Abstractions;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class ThreadIdPrepender : ITestOutputHelper
    {
        private readonly ITestOutputHelper _innerHelper;

        public ThreadIdPrepender(ITestOutputHelper innerHelper)
        {
            _innerHelper = innerHelper;
        }

        public void WriteLine(string message)
        {
            _innerHelper.WriteLine($"thread #{Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(2)} | {message}");
        }

        public void WriteLine(string format, params object[] args)
        {
            _innerHelper.WriteLine($"thread #{Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(2)} | {format}", args);
        }
    }
}