namespace MailingServiceDemo.ReadModel
{
    public interface IApplicationSettings
    {
        int MaxAttempt { get; }
    }

    public class FakeApplicationSettings : IApplicationSettings
    {
        public int MaxAttempt => 3;
    }
}
