using System;
using Xunit.Abstractions;

namespace MailingServiceDemo.Tests.AsyncTests
{
    public class TimeKeeper : ITestOutputHelper
    {
        private readonly ITestOutputHelper _innerHelper;

        public TimeKeeper(ITestOutputHelper innerHelper)
        {
            _innerHelper = innerHelper;
        }

        public void WriteLine(string message)
        {
            _innerHelper.WriteLine($"{DateTime.UtcNow.ToString("mm:ss.fff")} | {message}");
        }

        public void WriteLine(string format, params object[] args)
        {
            _innerHelper.WriteLine($"{DateTime.UtcNow.ToString("mm:ss.fff")} | {format}", args);
        }
    }
}