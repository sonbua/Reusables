using System;
using System.Collections.Generic;
using System.IO;

namespace Reusables.Adapter.System.IO
{
    public interface IDirectory
    {
        /// <summary>Determines whether the given path refers to an existing directory on disk.</summary>
        /// <returns>true if <paramref name="path" /> refers to an existing directory; false if the directory does not exist or an error occurs when trying to determine if the specified file exists.</returns>
        /// <param name="path">The path to test. </param>
        bool Exists(string path);

        /// <summary>Sets the creation date and time for the specified file or directory.</summary>
        /// <param name="path">The file or directory for which to set the creation date and time information. </param>
        /// <param name="creationTime">The date and time the file or directory was last written to. This value is expressed in local time.</param>
        void SetCreationTime(string path, DateTime creationTime);

        /// <summary>Sets the creation date and time, in Coordinated Universal Time (UTC) format, for the specified file or directory.</summary>
        /// <param name="path">The file or directory for which to set the creation date and time information. </param>
        /// <param name="creationTimeUtc">The date and time the directory or file was created. This value is expressed in local time.</param>
        void SetCreationTimeUtc(string path, DateTime creationTimeUtc);

        /// <summary>Gets the creation date and time of a directory.</summary>
        /// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in local time.</returns>
        /// <param name="path">The path of the directory. </param>
        DateTime GetCreationTime(string path);

        /// <summary>Gets the creation date and time, in Coordinated Universal Time (UTC) format, of a directory.</summary>
        /// <returns>A structure that is set to the creation date and time for the specified directory. This value is expressed in UTC time.</returns>
        /// <param name="path">The path of the directory. </param>
        DateTime GetCreationTimeUtc(string path);

        /// <summary>Sets the date and time a directory was last written to.</summary>
        /// <param name="path">The path of the directory. </param>
        /// <param name="lastWriteTime">The date and time the directory was last written to. This value is expressed in local time.  </param>
        void SetLastWriteTime(string path, DateTime lastWriteTime);

        /// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that a directory was last written to.</summary>
        /// <param name="path">The path of the directory. </param>
        /// <param name="lastWriteTimeUtc">The date and time the directory was last written to. This value is expressed in UTC time. </param>
        void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);

        /// <summary>Returns the date and time the specified file or directory was last written to.</summary>
        /// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain modification date and time information. </param>
        DateTime GetLastWriteTime(string path);

        /// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last written to.</summary>
        /// <returns>A structure that is set to the date and time the specified file or directory was last written to. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain modification date and time information. </param>
        DateTime GetLastWriteTimeUtc(string path);

        /// <summary>Sets the date and time the specified file or directory was last accessed.</summary>
        /// <param name="path">The file or directory for which to set the access date and time information. </param>
        /// <param name="lastAccessTime">An object that contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in local time. </param>
        void SetLastAccessTime(string path, DateTime lastAccessTime);

        /// <summary>Sets the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
        /// <param name="path">The file or directory for which to set the access date and time information. </param>
        /// <param name="lastAccessTimeUtc">An object that  contains the value to set for the access date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
        void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);

        /// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
        /// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain access date and time information. </param>
        DateTime GetLastAccessTime(string path);

        /// <summary>Returns the date and time, in Coordinated Universal Time (UTC) format, that the specified file or directory was last accessed.</summary>
        /// <returns>A structure that is set to the date and time the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain access date and time information. </param>
        DateTime GetLastAccessTimeUtc(string path);

        /// <summary>Returns the names of files (including their paths) in the specified directory.</summary>
        /// <returns>An array of the full names (including paths) for the files in the specified directory, or an empty array if no files are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        string[] GetFiles(string path);

        /// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory.</summary>
        /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern, or an empty array if no files are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        string[] GetFiles(string path, string searchPattern);

        /// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.</summary>
        /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern and option, or an empty array if no files are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory. </param>
        string[] GetFiles(string path, string searchPattern, SearchOption searchOption);

        /// <summary>Returns the names of subdirectories (including their paths) in the specified directory.</summary>
        /// <returns>An array of the full names (including paths) of subdirectories in the specified path, or an empty array if no directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        string[] GetDirectories(string path);

        /// <summary>Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory.</summary>
        /// <returns>An array of the full names (including paths) of the subdirectories that match the search pattern in the specified directory, or an empty array if no directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of subdirectories in <paramref name="path" />. This parameter can contain a combination of valid literal and wildcard characters (see Remarks), but doesn't support regular expressions. </param>
        string[] GetDirectories(string path, string searchPattern);

        /// <summary>Returns the names of the subdirectories (including their paths) that match the specified search pattern in the specified directory, and optionally searches subdirectories.</summary>
        /// <returns>An array of the full names (including paths) of the subdirectories that match the specified criteria, or an empty array if no directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of subdirectories in <paramref name="path" />. This parameter can contain a combination of valid literal and wildcard characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory. </param>
        string[] GetDirectories(string path, string searchPattern, SearchOption searchOption);

        /// <summary>Returns the names of all files and subdirectories in a specified path.</summary>
        /// <returns>An array of the names of files and subdirectories in the specified directory, or an empty array if no files or subdirectories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        string[] GetFileSystemEntries(string path);

        /// <summary>Returns an array of file names and directory names that that match a search pattern in a specified path.</summary>
        /// <returns>An array of file names and directory names that match the specified search criteria, or an empty array if no files or directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of file and directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        string[] GetFileSystemEntries(string path, string searchPattern);

        /// <summary>Returns an array of all the file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <returns>An array of file the file names and directory names that match the specified search criteria, or an empty array if no files or directories are found.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files and directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
        string[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption);

        /// <summary>Returns an enumerable collection of directory names in a specified path.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" />.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        IEnumerable<string> EnumerateDirectories(string path);

        /// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        IEnumerable<string> EnumerateDirectories(string path, string searchPattern);

        /// <summary>Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the directories in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
        IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption);

        /// <summary>Returns an enumerable collection of file names in a specified path.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" />.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        IEnumerable<string> EnumerateFiles(string path);

        /// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions. </param>
        IEnumerable<string> EnumerateFiles(string path, string searchPattern);

        /// <summary>Returns an enumerable collection of file names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.  </param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
        IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);

        /// <summary>Returns an enumerable collection of file names and directory names in a specified path. </summary>
        /// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" />.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        IEnumerable<string> EnumerateFileSystemEntries(string path);

        /// <summary>Returns an enumerable collection of file names and directory names that  match a search pattern in a specified path.</summary>
        /// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive. </param>
        /// <param name="searchPattern">The search string to match against the names of file-system entries in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.  </param>
        IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern);

        /// <summary>Returns an enumerable collection of file names and directory names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <returns>An enumerable collection of file-system entries in the directory specified by <paramref name="path" /> and that match the specified search pattern and option.</returns>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against file-system entries in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values  that specifies whether the search operation should include only the current directory or should include all subdirectories.The default value is <see cref="F:System.IO.SearchOption.TopDirectoryOnly" />.</param>
        IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption);

        /// <summary>Retrieves the names of the logical drives on this computer in the form "&lt;drive letter&gt;:\".</summary>
        /// <returns>The logical drives on this computer.</returns>
        string[] GetLogicalDrives();

        /// <summary>Returns the volume information, root information, or both for the specified path.</summary>
        /// <returns>A string that contains the volume information, root information, or both for the specified path.</returns>
        /// <param name="path">The path of a file or directory. </param>
        string GetDirectoryRoot(string path);

        /// <summary>Gets the current working directory of the application.</summary>
        /// <returns>A string that contains the path of the current working directory, and does not end with a backslash (\).</returns>
        string GetCurrentDirectory();

        /// <summary>Sets the application's current working directory to the specified directory.</summary>
        /// <param name="path">The path to which the current working directory is set. </param>
        void SetCurrentDirectory(string path);

        /// <summary>Moves a file or a directory and its contents to a new location.</summary>
        /// <param name="sourceDirName">The path of the file or directory to move. </param>
        /// <param name="destDirName">The path to the new location for <paramref name="sourceDirName" />. If <paramref name="sourceDirName" /> is a file, then <paramref name="destDirName" /> must also be a file name.</param>
        void Move(string sourceDirName, string destDirName);

        /// <summary>Deletes an empty directory from a specified path.</summary>
        /// <param name="path">The name of the empty directory to remove. This directory must be writable and empty. </param>
        void Delete(string path);

        /// <summary>Deletes the specified directory and, if indicated, any subdirectories and files in the directory. </summary>
        /// <param name="path">The name of the directory to remove. </param>
        /// <param name="recursive">true to remove directories, subdirectories, and files in <paramref name="path" />; otherwise, false. </param>
        void Delete(string path, bool recursive);
    }
}