using System;
using System.Diagnostics;
using log4net;

namespace Reusables.Diagnostics.Logging.Log4net
{
    // ReSharper disable once InconsistentNaming
    public class Log4netLoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger<T>()
        {
            return new Log4netLogger(LogManager.GetLogger(typeof(T)));
        }

        public ILogger GetCurrentClassLogger()
        {
            return new Log4netLogger(LogManager.GetLogger(GetClassFullName()));
        }

        private static string GetClassFullName()
        {
            var skipFrames = 2;
            Type declaringType;
            string classFullName;

            do
            {
                var method = new StackFrame(skipFrames, false).GetMethod();

                declaringType = method.DeclaringType;

                if (declaringType == null)
                    return method.Name;

                ++skipFrames;
                classFullName = declaringType.FullName;
            } while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return classFullName;
        }
    }
}