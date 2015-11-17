namespace MailingServiceDemo.ReadModel
{
    public interface IApplicationSettings
    {
        int MaxRetry { get; }
    }

    public class FakeApplicationSettings : IApplicationSettings
    {
        public int MaxRetry => 3;
    }
}
