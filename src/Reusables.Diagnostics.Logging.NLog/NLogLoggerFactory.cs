using System;
using System.Diagnostics;
using NLog;

namespace Reusables.Diagnostics.Logging.NLog
{
    public class NLogLoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger<T>()
        {
            return new NLogLogger(LogManager.GetLogger(typeof(T).FullName));
        }

        public ILogger GetCurrentClassLogger()
        {
            return new NLogLogger(LogManager.GetLogger(GetClassFullName()));
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