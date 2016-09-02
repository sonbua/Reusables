using System;
using Reusables.Diagnostics.Logging;
using Xunit.Abstractions;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class TestOutputLogger : ILogger
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TestOutputLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public void Debug(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Debug(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] args)
        {
            _testOutputHelper.WriteLine($"{message}");
        }

        public void Warn(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception exception)
        {
            _testOutputHelper.WriteLine($">> ERROR: {exception}");
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _testOutputHelper.WriteLine($">> ERROR: {exception.GetType().Name}: {exception.Message}\n{message}");
        }

        public void Error(string message, params object[] args)
        {
            _testOutputHelper.WriteLine(message);
        }

        public void Fatal(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}