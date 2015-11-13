using System;
using Reusables.Diagnostics.Contracts;

namespace Reusables.Util.Extensions
{
    /// <summary>
    /// All extension methods related to conversion.
    /// </summary>
    public static class ConversionExtensions
    {
        /// <summary>
        /// Convert string to byte array.
        /// </summary>
        /// <param name="str">The string to be converted to byte array.</param>
        /// <returns></returns>
        public static byte[] GetBytes(this string str)
        {
            Requires.IsNotNull(str, nameof(str));

            var bytes = new byte[str.Length*sizeof (char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Convert byte array to string.
        /// </summary>
        /// <param name="bytes">The byte array to be coverted to string.</param>
        /// <returns></returns>
        public static string GetString(this byte[] bytes)
        {
            Requires.IsNotNull(bytes, nameof(bytes));

            var chars = new char[bytes.Length/sizeof (char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
