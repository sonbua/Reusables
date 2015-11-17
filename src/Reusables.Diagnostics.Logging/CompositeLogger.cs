using System;
using System.Collections.Generic;

namespace Reusables.Diagnostics.Logging
{
    public class CompositeLogger : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        public CompositeLogger(IEnumerable<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void Debug(Exception exception)
        {
            foreach (var logger in _loggers)
            {
                logger.Debug(exception);
            }
        }

        public void Debug(Exception exception, string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Debug(exception, message, args);
            }
        }

        public void Debug(string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Debug(message, args);
            }
        }

        public void Info(Exception exception)
        {
            foreach (var logger in _loggers)
            {
                logger.Info(exception);
            }
        }

        public void Info(Exception exception, string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Info(exception, message, args);
            }
        }

        public void Info(string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Info(message, args);
            }
        }

        public void Warn(Exception exception)
        {
            foreach (var logger in _loggers)
            {
                logger.Warn(exception);
            }
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Warn(exception, message, args);
            }
        }

        public void Warn(string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Warn(message, args);
            }
        }

        public void Error(Exception exception)
        {
            foreach (var logger in _loggers)
            {
                logger.Error(exception);
            }
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Error(exception, message, args);
            }
        }

        public void Error(string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Error(message, args);
            }
        }

        public void Fatal(Exception exception)
        {
            foreach (var logger in _loggers)
            {
                logger.Fatal(exception);
            }
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Fatal(exception, message, args);
            }
        }

        public void Fatal(string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Fatal(message, args);
            }
        }
    }
}
