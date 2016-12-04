﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Reusables.Diagnostics.Contracts;

namespace Reusables.Util.Extensions
{
    /// <summary>
    /// All extension methods related to String.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Clean up all invalid characters in a given string, e.g. <c>" | * ? :</c>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CleanUpInvalidFileNameChars(this string fileName)
        {
            return new string(fileName.CleanUpInvalidFileNameCharsImpl().ToArray());
        }

        private static IEnumerable<char> CleanUpInvalidFileNameCharsImpl(this string fileName)
        {
            return fileName.Where(c => !c.IsInvalidFileNameChar());
        }

        private static bool IsInvalidFileNameChar(this char c)
        {
            return c == '"' || c == '<' || c == '>' || c == '|' || c == '*' || c == '?' || c == ':' || c < 32;
        }

        /// <summary>
        /// Determines whether this string and a specified <see cref="T:System.String"/> object have the same value, ignore their casing.
        /// </summary>
        /// <param name="subject">The subject string to be compared.</param>
        /// <param name="other">The string to compare to this instance.</param>
        /// <returns>true if the value of the <paramref name="other"/> parameter is the same as this string; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="subject"/> is null.</exception>
        public static bool EqualsIgnoreCase(this string subject, string other)
        {
            Requires.IsNotNull(subject, nameof(subject));

            return subject.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Formats an input string according to a given mask.
        /// </summary>
        /// <param name="source">The input string.</param>
        /// <param name="mask">The mask should contain # and other character. # stands for a character in the input string.
        /// For example: Formatting 123456 with mask ###-### results in 123-456</param>
        /// <returns>The formatted string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="mask"/> is null.</exception>
        public static string FormatWithMask(this string source, string mask)
        {
            Requires.IsNotNull(source, nameof(source));
            Requires.IsNotNull(mask, nameof(mask));

            var maskedInput = string.Empty;
            var index = 0;

            foreach (var maskElement in mask)
            {
                if (maskElement != '#')
                {
                    maskedInput += maskElement;
                    continue;
                }

                if (index >= source.Length)
                    continue;

                maskedInput += source[index];
                index++;
            }

            return maskedInput;
        }

        /// <summary>
        /// Indicates whether the specified string is null or an <see cref="string.Empty"/> string.
        /// </summary>
        /// <param name="source">The string to test.</param>
        /// <returns>
        /// true if the <paramref name="source"/> parameter is null or an empty string (""); otherwise, false.
        /// </returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Fills a template with data object.
        /// For example, merging this string
        /// <code>{Name} is {Age} years old</code>
        /// with this person
        /// <code>var john = new Person {Name = "John Doe", Age = 30};</code>
        /// will result
        /// <code>John Doe is 30 years old</code>
        /// Does not support types that contain public static member(s), and will throw <see cref="ArgumentOutOfRangeException"/> in this case.
        /// </summary>
        /// <param name="template">The template string that will be filled with <paramref name="data"/>.</param>
        /// <param name="data">The data object that will be populated in the <paramref name="template"/>.</param>
        /// <param name="fieldStartDelimiter">The start delimiter of field template. Default is <c>{</c>.</param>
        /// <param name="fieldEndDelimiter">The end delimiter of field template. Default is <c>}</c>.</param>
        /// <returns></returns>
        public static string Merge(this string template, object data, char fieldStartDelimiter = '{', char fieldEndDelimiter = '}')
        {
            Requires.IsNotNull(template, nameof(template));
            Requires.IsNotNull(data, nameof(data));

            var dataDictionary = data.ToDictionary();

            return dataDictionary.Keys
                                 .Where(x => x != null)
                                 .Aggregate(template, (current, key) =>
                                                      {
                                                          var fieldTemplate = fieldStartDelimiter + key + fieldEndDelimiter;
                                                          var fieldValue = dataDictionary[key].ToString();

                                                          return current.Replace(fieldTemplate, fieldValue);
                                                      });
        }

        /// <summary>
        /// In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Specified options modify the matching operation.
        /// </summary>
        /// <param name="source">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
        /// <exception cref="ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="pattern"/>, or <paramref name="replacement"/> is null.</exception>
        public static string RegexReplace(this string source, string pattern, string replacement)
        {
            return source.RegexReplace(pattern, replacement, RegexOptions.None);
        }

        /// <summary>
        /// In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Specified options modify the matching operation.
        /// </summary>
        /// <param name="source">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
        /// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string.</returns>
        /// <exception cref="ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="pattern"/>, or <paramref name="replacement"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> is not a valid bitwise combination of <see cref="RegexOptions"/> values.</exception>
        public static string RegexReplace(this string source, string pattern, string replacement, RegexOptions options)
        {
            return Regex.Replace(source, pattern, replacement, options);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of specified characters in the current instance are removed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="removedChars">The characters to be removed.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of <paramref name="removedChars"/> are removed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="removedChars"/> is null.</exception>
        public static string Remove(this string source, params char[] removedChars)
        {
            Requires.IsNotNull(source, nameof(source));
            Requires.IsNotNull(removedChars, nameof(removedChars));

            if (removedChars.Length == 0)
                return source;

            return source.ReplaceImpl(removedChars, string.Empty);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of specified characters in the current instance are removed.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="removedStrings">The characters to be removed.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of <paramref name="removedStrings"/> are removed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="removedStrings"/> is null.</exception>
        public static string Remove(this string source, params string[] removedStrings)
        {
            Requires.IsNotNull(source, nameof(source));
            Requires.IsNotNull(removedStrings, nameof(removedStrings));

            if (source.Length == 0 || removedStrings.Length == 0)
                return source;

            return source.Replace(removedStrings, string.Empty);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of specified characters in the current instance are replaced with another specified string.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="oldChars">The characters to be replaced.</param>
        /// <param name="substituent">The string to replace all occurrences of <paramref name="oldChars"/>.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of <paramref name="oldChars"/> are replaced with <paramref name="substituent"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="oldChars"/> is null.</exception>
        public static string Replace(this string source, char[] oldChars, string substituent)
        {
            Requires.IsNotNull(source, nameof(source));
            Requires.IsNotNull(oldChars, nameof(oldChars));

            if (source.Length == 0 || oldChars.Length == 0)
                return source;

            return source.ReplaceImpl(oldChars, substituent);
        }

        private static string ReplaceImpl(this string source, IEnumerable<char> oldChars, string substituent)
        {
            var builder = new StringBuilder(source);
            foreach (var c in oldChars)
                builder.Replace(c.ToString(), substituent);
            return builder.ToString();
        }

        /// <summary>
        /// Returns a new string in which all occurrences of specified strings in the current instance are replaced with another specified string.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="oldStrings">The strings to be replaced.</param>
        /// <param name="substituent">The string to replace all occurrences of <paramref name="oldStrings"/>.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of <paramref name="oldStrings"/> are replaced with <paramref name="substituent"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="oldStrings"/> is null.</exception>
        public static string Replace(this string source, string[] oldStrings, string substituent)
        {
            Requires.IsNotNull(source, nameof(source));
            Requires.IsNotNull(oldStrings, nameof(oldStrings));

            return source.ReplaceImpl(oldStrings, substituent);
        }

        private static string ReplaceImpl(this string source, IEnumerable<string> oldStrings, string substituent)
        {
            var builder = new StringBuilder(source);
            foreach (var oldString in oldStrings)
                builder.Replace(oldString, substituent);
            return builder.ToString();
        }

        /// <summary>
        /// Safely replaces one or more format items in a specified string with the string representation of a specified object. Originally used to format string to write to log.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A copy of <paramref name="format"/> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="format"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array.</exception>
        public static string SafeFormat(this string format, params object[] args)
        {
            if (args == null)
                return string.Format(format, string.Empty);
            if (args.Length == 0)
                return format;

            var safeArgs = args.Select(arg => arg ?? string.Empty).ToArray();
            return string.Format(format, safeArgs);
        }
    }
}
