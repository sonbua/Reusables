using Reusables.Diagnostics.Logging;

namespace MailingServiceDemo.Tests.SyncTests
{
    public class TestOutputLoggerFactory : ILoggerFactory
    {
        private readonly ILogger _logger;

        public TestOutputLoggerFactory(ILogger logger)
        {
            _logger = logger;
        }

        public ILogger GetLogger<T>()
        {
            return GetCurrentClassLogger();
        }

        public ILogger GetCurrentClassLogger()
        {
            return _logger;
        }
    }
}