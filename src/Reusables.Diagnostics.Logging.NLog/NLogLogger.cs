using System;
using NLog;

namespace Reusables.Diagnostics.Logging.NLog
{
    public class NLogLogger : ILogger
    {
        private readonly Logger _innerLogger;

        public NLogLogger(Logger innerLogger)
        {
            _innerLogger = innerLogger;
        }

        public void Debug(Exception exception)
        {
            _innerLogger.Debug(exception);
        }

        public void Debug(Exception exception, string message, params object[] args)
        {
            _innerLogger.Debug(exception, message, args);
        }

        public void Debug(string message, params object[] args)
        {
            _innerLogger.Debug(message, args);
        }

        public void Info(Exception exception)
        {
            _innerLogger.Info(exception);
        }

        public void Info(Exception exception, string message, params object[] args)
        {
            _innerLogger.Info(exception, message, args);
        }

        public void Info(string message, params object[] args)
        {
            _innerLogger.Info(message, args);
        }

        public void Warn(Exception exception)
        {
            _innerLogger.Warn(exception);
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            _innerLogger.Warn(exception, message, args);
        }

        public void Warn(string message, params object[] args)
        {
            _innerLogger.Warn(message, args);
        }

        public void Error(Exception exception)
        {
            _innerLogger.Error(exception);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _innerLogger.Error(exception, message, args);
        }

        public void Error(string message, params object[] args)
        {
            _innerLogger.Error(message, args);
        }

        public void Fatal(Exception exception)
        {
            _innerLogger.Fatal(exception);
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            _innerLogger.Fatal(exception, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            _innerLogger.Fatal(message, args);
        }
    }
}
