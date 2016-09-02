namespace MailingServiceDemo.Tests.SyncTests
{
    public class FakeApplicationSettings : IApplicationSettings
    {
        public int MaxAttempt => 3;
    }
}