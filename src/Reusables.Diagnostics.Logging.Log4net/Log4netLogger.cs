using System;
using log4net;

namespace Reusables.Diagnostics.Logging.Log4net
{
    // ReSharper disable once InconsistentNaming
    public class Log4netLogger : ILogger
    {
        private readonly ILog _innerLogger;

        public Log4netLogger(ILog innerLogger)
        {
            _innerLogger = innerLogger;
        }

        public void Debug(Exception exception)
        {
            _innerLogger.Debug(exception);
        }

        public void Debug(Exception exception, string message, params object[] args)
        {
            _innerLogger.Debug(string.Format(message, args), exception);
        }

        public void Debug(string message, params object[] args)
        {
            _innerLogger.Debug(string.Format(message, args));
        }

        public void Info(Exception exception)
        {
            _innerLogger.Info(exception);
        }

        public void Info(Exception exception, string message, params object[] args)
        {
            _innerLogger.Info(string.Format(message, args), exception);
        }

        public void Info(string message, params object[] args)
        {
            _innerLogger.Info(string.Format(message, args));
        }

        public void Warn(Exception exception)
        {
            _innerLogger.Warn(exception);
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            _innerLogger.Warn(string.Format(message, args), exception);
        }

        public void Warn(string message, params object[] args)
        {
            _innerLogger.Warn(string.Format(message, args));
        }

        public void Error(Exception exception)
        {
            _innerLogger.Error(exception);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _innerLogger.Error(string.Format(message, args), exception);
        }

        public void Error(string message, params object[] args)
        {
            _innerLogger.Error(string.Format(message, args));
        }

        public void Fatal(Exception exception)
        {
            _innerLogger.Fatal(exception);
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            _innerLogger.Fatal(string.Format(message, args), exception);
        }

        public void Fatal(string message, params object[] args)
        {
            _innerLogger.Fatal(string.Format(message, args));
        }
    }
}