using Xunit.Abstractions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class NewLineAppender : ITestOutputHelper
    {
        private readonly ITestOutputHelper _innerHelper;

        public NewLineAppender(ITestOutputHelper innerHelper)
        {
            _innerHelper = innerHelper;
        }

        public void WriteLine(string message)
        {
            _innerHelper.WriteLine($"{message}\n");
        }

        public void WriteLine(string format, params object[] args)
        {
            _innerHelper.WriteLine($"{format}\n", args);
        }
    }
}