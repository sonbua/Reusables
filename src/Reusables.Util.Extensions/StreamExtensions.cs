using System.IO;
using Reusables.Diagnostics.Contracts;

namespace Reusables.Util.Extensions
{
    public static class StreamExtensions
    {
        public static bool TryToRewind(this Stream stream)
        {
            Requires.IsNotNull(stream, nameof(stream));

            if (stream.CanSeek)
            {
                stream.Position = 0;

                return true;
            }

            return false;
        }
    }
}
