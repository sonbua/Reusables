namespace Reusables.Diagnostics.Logging
{
    public interface ILoggerFactory
    {
        /// <summary>
        /// Gets the logger with the name of the class <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to get the logger.</typeparam>
        /// <returns></returns>
        ILogger GetLogger<T>();

        /// <summary>
        /// Gets the logger with the name of the current class.
        /// </summary>
        /// <returns></returns>
        ILogger GetCurrentClassLogger();
    }
}