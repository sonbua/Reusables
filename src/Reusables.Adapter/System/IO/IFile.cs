using System;
using System.Collections.Generic;
using System.Text;

namespace Reusables.Adapter.System.IO
{
    public interface IFile
    {
        /// <summary>Copies an existing file to a new file. Overwriting a file of the same name is not allowed.</summary>
        /// <param name="sourceFileName">The file to copy. </param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file. </param>
        void Copy(string sourceFileName, string destFileName);

        /// <summary>Copies an existing file to a new file. Overwriting a file of the same name is allowed.</summary>
        /// <param name="sourceFileName">The file to copy. </param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory. </param>
        /// <param name="overwrite">true if the destination file can be overwritten; otherwise, false. </param>
        void Copy(string sourceFileName, string destFileName, bool overwrite);

        /// <summary>Deletes the specified file. </summary>
        /// <param name="path">The name of the file to be deleted. Wildcard characters are not supported.</param>
        void Delete(string path);

        /// <summary>Decrypts a file that was encrypted by the current account using the <see cref="M:System.IO.File.Encrypt(System.String)" /> method.</summary>
        /// <param name="path">A path that describes a file to decrypt.</param>
        void Decrypt(string path);

        /// <summary>Encrypts a file so that only the account used to encrypt the file can decrypt it.</summary>
        /// <param name="path">A path that describes a file to encrypt.</param>
        void Encrypt(string path);

        /// <summary>Determines whether the specified file exists.</summary>
        /// <returns>true if the caller has the required permissions and <paramref name="path" /> contains the name of an existing file; otherwise, false. This method also returns false if <paramref name="path" /> is null, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns false regardless of the existence of <paramref name="path" />.</returns>
        /// <param name="path">The file to check. </param>
        bool Exists(string path);

        /// <summary>Sets the date and time the file was created.</summary>
        /// <param name="path">The file for which to set the creation date and time information. </param>
        /// <param name="creationTime">A <see cref="T:System.DateTime" /> containing the value to set for the creation date and time of <paramref name="path" />. This value is expressed in local time. </param>
        void SetCreationTime(string path, DateTime creationTime);

        /// <summary>Sets the date and time, in coordinated universal time (UTC), that the file was created.</summary>
        /// <param name="path">The file for which to set the creation date and time information. </param>
        /// <param name="creationTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the creation date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
        void SetCreationTimeUtc(string path, DateTime creationTimeUtc);

        /// <summary>Returns the creation date and time of the specified file or directory.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the creation date and time for the specified file or directory. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain creation date and time information. </param>
        DateTime GetCreationTime(string path);

        /// <summary>Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the creation date and time for the specified file or directory. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain creation date and time information. </param>
        DateTime GetCreationTimeUtc(string path);

        /// <summary>Sets the date and time the specified file was last accessed.</summary>
        /// <param name="path">The file for which to set the access date and time information. </param>
        /// <param name="lastAccessTime">A <see cref="T:System.DateTime" /> containing the value to set for the last access date and time of <paramref name="path" />. This value is expressed in local time. </param>
        void SetLastAccessTime(string path, DateTime lastAccessTime);

        /// <summary>Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.</summary>
        /// <param name="path">The file for which to set the access date and time information. </param>
        /// <param name="lastAccessTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the last access date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
        void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);

        /// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain access date and time information. </param>
        DateTime GetLastAccessTime(string path);

        /// <summary>Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain access date and time information. </param>
        DateTime GetLastAccessTimeUtc(string path);

        /// <summary>Sets the date and time that the specified file was last written to.</summary>
        /// <param name="path">The file for which to set the date and time information. </param>
        /// <param name="lastWriteTime">A <see cref="T:System.DateTime" /> containing the value to set for the last write date and time of <paramref name="path" />. This value is expressed in local time. </param>
        void SetLastWriteTime(string path, DateTime lastWriteTime);

        /// <summary>Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.</summary>
        /// <param name="path">The file for which to set the date and time information. </param>
        /// <param name="lastWriteTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the last write date and time of <paramref name="path" />. This value is expressed in UTC time. </param>
        void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);

        /// <summary>Returns the date and time the specified file or directory was last written to.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to. This value is expressed in local time.</returns>
        /// <param name="path">The file or directory for which to obtain write date and time information. </param>
        DateTime GetLastWriteTime(string path);

        /// <summary>Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to. This value is expressed in UTC time.</returns>
        /// <param name="path">The file or directory for which to obtain write date and time information. </param>
        DateTime GetLastWriteTimeUtc(string path);

        /// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
        /// <returns>A string containing all lines of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        string ReadAllText(string path);

        /// <summary>Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
        /// <returns>A string containing all lines of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        /// <param name="encoding">The encoding applied to the contents of the file. </param>
        string ReadAllText(string path, Encoding encoding);

        /// <summary>Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.</summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string to write to the file. </param>
        void WriteAllText(string path, string contents);

        /// <summary>Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.</summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string to write to the file. </param>
        /// <param name="encoding">The encoding to apply to the string.</param>
        void WriteAllText(string path, string contents, Encoding encoding);

        /// <summary>Opens a binary file, reads the contents of the file into a byte array, and then closes the file.</summary>
        /// <returns>A byte array containing the contents of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        byte[] ReadAllBytes(string path);

        /// <summary>Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.</summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="bytes">The bytes to write to the file. </param>
        void WriteAllBytes(string path, byte[] bytes);

        /// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
        /// <returns>A string array containing all lines of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        string[] ReadAllLines(string path);

        /// <summary>Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
        /// <returns>A string array containing all lines of the file.</returns>
        /// <param name="path">The file to open for reading. </param>
        /// <param name="encoding">The encoding applied to the contents of the file. </param>
        string[] ReadAllLines(string path, Encoding encoding);

        /// <summary>Reads the lines of a file.</summary>
        /// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
        /// <param name="path">The file to read.</param>
        IEnumerable<string> ReadLines(string path);

        /// <summary>Read the lines of a file that has a specified encoding.</summary>
        /// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
        /// <param name="path">The file to read.</param>
        /// <param name="encoding">The encoding that is applied to the contents of the file. </param>
        IEnumerable<string> ReadLines(string path, Encoding encoding);

        /// <summary>Creates a new file, write the specified string array to the file, and then closes the file. </summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string array to write to the file. </param>
        void WriteAllLines(string path, string[] contents);

        /// <summary>Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file. </summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string array to write to the file. </param>
        /// <param name="encoding">An <see cref="T:System.Text.Encoding" /> object that represents the character encoding applied to the string array.</param>
        void WriteAllLines(string path, string[] contents, Encoding encoding);

        /// <summary>Creates a new file, writes a collection of strings to the file, and then closes the file.</summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The lines to write to the file.</param>
        void WriteAllLines(string path, IEnumerable<string> contents);

        /// <summary>Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.</summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The lines to write to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        /// <summary>Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.</summary>
        /// <param name="path">The file to append the specified string to. </param>
        /// <param name="contents">The string to append to the file. </param>
        void AppendAllText(string path, string contents);

        /// <summary>Appends the specified string to the file, creating the file if it does not already exist.</summary>
        /// <param name="path">The file to append the specified string to. </param>
        /// <param name="contents">The string to append to the file. </param>
        /// <param name="encoding">The character encoding to use. </param>
        void AppendAllText(string path, string contents, Encoding encoding);

        /// <summary>Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.</summary>
        /// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
        /// <param name="contents">The lines to append to the file.</param>
        void AppendAllLines(string path, IEnumerable<string> contents);

        /// <summary>Appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.</summary>
        /// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
        /// <param name="contents">The lines to append to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        /// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
        /// <param name="sourceFileName">The name of the file to move. Can include a relative or absolute path.</param>
        /// <param name="destFileName">The new path and name for the file.</param>
        void Move(string sourceFileName, string destFileName);

        /// <summary>Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file.</summary>
        /// <param name="sourceFileName">The name of a file that replaces the file specified by <paramref name="destinationFileName" />.</param>
        /// <param name="destinationFileName">The name of the file being replaced.</param>
        /// <param name="destinationBackupFileName">The name of the backup file.</param>
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName);

        /// <summary>Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file and optionally ignores merge errors.</summary>
        /// <param name="sourceFileName">The name of a file that replaces the file specified by <paramref name="destinationFileName" />.</param>
        /// <param name="destinationFileName">The name of the file being replaced.</param>
        /// <param name="destinationBackupFileName">The name of the backup file.</param>
        /// <param name="ignoreMetadataErrors">true to ignore merge errors (such as attributes and access control lists (ACLs)) from the replaced file to the replacement file; otherwise, false. </param>
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);
    }
}