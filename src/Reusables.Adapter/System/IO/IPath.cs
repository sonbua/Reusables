namespace Reusables.Adapter.System.IO
{
    public interface IPath
    {
        /// <summary>Changes the extension of a path string.</summary>
        /// <returns>The modified path information.On Windows-based desktop platforms, if <paramref name="path" /> is null or an empty string (""), the path information is returned unmodified. If <paramref name="extension" /> is null, the returned string contains the specified path with its extension removed. If <paramref name="path" /> has no extension, and <paramref name="extension" /> is not null, the returned path string contains <paramref name="extension" /> appended to the end of <paramref name="path" />.</returns>
        /// <param name="path">The path information to modify. The path cannot contain any of the characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />. </param>
        /// <param name="extension">The new extension (with or without a leading period). Specify null to remove an existing extension from <paramref name="path" />. </param>
        string ChangeExtension(string path, string extension);

        /// <summary>Returns the directory information for the specified path string.</summary>
        /// <returns>Directory information for <paramref name="path" />, or null if <paramref name="path" /> denotes a root directory or is null. Returns <see cref="F:System.String.Empty" /> if <paramref name="path" /> does not contain directory information.</returns>
        /// <param name="path">The path of a file or directory. </param>
        string GetDirectoryName(string path);

        /// <summary>Gets an array containing the characters that are not allowed in path names.</summary>
        /// <returns>An array containing the characters that are not allowed in path names.</returns>
        char[] GetInvalidPathChars();

        /// <summary>Gets an array containing the characters that are not allowed in file names.</summary>
        /// <returns>An array containing the characters that are not allowed in file names.</returns>
        char[] GetInvalidFileNameChars();

        /// <summary>Returns the extension of the specified path string.</summary>
        /// <returns>The extension of the specified path (including the period "."), or null, or <see cref="F:System.String.Empty" />. If <paramref name="path" /> is null, <see cref="M:System.IO.Path.GetExtension(System.String)" /> returns null. If <paramref name="path" /> does not have extension information, <see cref="M:System.IO.Path.GetExtension(System.String)" /> returns <see cref="F:System.String.Empty" />.</returns>
        /// <param name="path">The path string from which to get the extension. </param>
        string GetExtension(string path);

        /// <summary>Returns the absolute path for the specified path string.</summary>
        /// <returns>The fully qualified location of <paramref name="path" />, such as "C:\MyFile.txt".</returns>
        /// <param name="path">The file or directory for which to obtain absolute path information. </param>
        string GetFullPath(string path);

        /// <summary>Returns the file name and extension of the specified path string.</summary>
        /// <returns>The characters after the last directory character in <paramref name="path" />. If the last character of <paramref name="path" /> is a directory or volume separator character, this method returns <see cref="F:System.String.Empty" />. If <paramref name="path" /> is null, this method returns null.</returns>
        /// <param name="path">The path string from which to obtain the file name and extension. </param>
        string GetFileName(string path);

        /// <summary>Returns the file name of the specified path string without the extension.</summary>
        /// <returns>The string returned by <see cref="M:System.IO.Path.GetFileName(System.String)" />, minus the last period (.) and all characters following it.</returns>
        /// <param name="path">The path of the file. </param>
        string GetFileNameWithoutExtension(string path);

        /// <summary>Gets the root directory information of the specified path.</summary>
        /// <returns>The root directory of <paramref name="path" />, such as "C:\", or null if <paramref name="path" /> is null, or an empty string if <paramref name="path" /> does not contain root directory information.</returns>
        /// <param name="path">The path from which to obtain root directory information. </param>
        string GetPathRoot(string path);

        /// <summary>Returns the path of the current user's temporary folder.</summary>
        /// <returns>The path to the temporary folder, ending with a backslash.</returns>
        string GetTempPath();

        /// <summary>Returns a random folder name or file name.</summary>
        /// <returns>A random folder name or file name.</returns>
        string GetRandomFileName();

        /// <summary>Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.</summary>
        /// <returns>The full path of the temporary file.</returns>
        string GetTempFileName();

        /// <summary>Determines whether a path includes a file name extension.</summary>
        /// <returns>true if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, false.</returns>
        /// <param name="path">The path to search for an extension. </param>
        bool HasExtension(string path);

        /// <summary>Gets a value indicating whether the specified path string contains a root.</summary>
        /// <returns>true if <paramref name="path" /> contains a root; otherwise, false.</returns>
        /// <param name="path">The path to test. </param>
        bool IsPathRooted(string path);

        /// <summary>Combines two strings into a path.</summary>
        /// <returns>The combined paths. If one of the specified paths is a zero-length string, this method returns the other path. If <paramref name="path2" /> contains an absolute path, this method returns <paramref name="path2" />.</returns>
        /// <param name="path1">The first path to combine. </param>
        /// <param name="path2">The second path to combine. </param>
        string Combine(string path1, string path2);

        /// <summary>Combines three strings into a path.</summary>
        /// <returns>The combined paths.</returns>
        /// <param name="path1">The first path to combine. </param>
        /// <param name="path2">The second path to combine. </param>
        /// <param name="path3">The third path to combine.</param>
        string Combine(string path1, string path2, string path3);

        /// <summary>Combines four strings into a path.</summary>
        /// <returns>The combined paths.</returns>
        /// <param name="path1">The first path to combine. </param>
        /// <param name="path2">The second path to combine. </param>
        /// <param name="path3">The third path to combine.</param>
        /// <param name="path4">The fourth path to combine.</param>
        string Combine(string path1, string path2, string path3, string path4);

        /// <summary>Combines an array of strings into a path.</summary>
        /// <returns>The combined paths.</returns>
        /// <param name="paths">An array of parts of the path.</param>
        string Combine(params string[] paths);
    }
}