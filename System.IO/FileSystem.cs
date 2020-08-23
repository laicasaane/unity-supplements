using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Text;
using System.Security.Cryptography;

namespace System.IO
{
    public static class FileSystem
    {
        private static readonly BinaryFormatter _binaryFormatter;

        static FileSystem()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public static bool FileExists(string filePath)
            => File.Exists(filePath);

        public static bool DirectoryExists(string directoryPath)
            => Directory.Exists(directoryPath);

        public static void CreateDirectory(string directoryPath)
            => Directory.CreateDirectory(directoryPath);

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (var stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                _binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, ICryptoTransform encryptor, bool append = false)
        {
            using (var innerStream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                using (var cryptoStream = new CryptoStream(innerStream, encryptor, CryptoStreamMode.Write))
                {
                    _binaryFormatter.Serialize(cryptoStream, objectToWrite);
                }
            }
        }

        public static void WriteToBinaryFile<T>(string directoryPath, string filePath, T objectToWrite, bool append = false)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            WriteToBinaryFile(filePath, objectToWrite, append);
        }

        public static void WriteToBinaryFile<T>(string directoryPath, string filePath, T objectToWrite, ICryptoTransform encryptor, bool append = false)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            WriteToBinaryFile(filePath, objectToWrite, encryptor, append);
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <param name="default">The default value to return if the file cannot be read.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath, T @default = default)
        {
            if (File.Exists(filePath))
            {
                using (var stream = File.Open(filePath, FileMode.Open))
                {
                    return (T)_binaryFormatter.Deserialize(stream);
                }
            }

            return @default;
        }

        public static T ReadFromBinaryFile<T>(string filePath, ICryptoTransform decryptor, T @default = default)
        {
            if (File.Exists(filePath))
            {
                using (var innerStream = File.Open(filePath, FileMode.Open))
                {
                    using (var cryptoStream = new CryptoStream(innerStream, decryptor, CryptoStreamMode.Read))
                    {
                        return (T)_binaryFormatter.Deserialize(cryptoStream);
                    }
                }
            }

            return @default;
        }

        public static T ReadFromBinaryFile<T>(string directoryPath, string filePath, T @default = default)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            return ReadFromBinaryFile(filePath, @default);
        }

        public static T ReadFromBinaryFile<T>(string directoryPath, string filePath, ICryptoTransform decryptor, T @default = default)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            return ReadFromBinaryFile(filePath, decryptor, @default);
        }

        /// <summary>
        /// Writes the given object instance to a text file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the text file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the text file.</param>
        /// <param name="formatter">The formatter to serialize the object.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToTextFile<T>(string filePath, T objectToWrite, ITextFormatter formatter, bool append = false)
        {
            using (var writer = new StreamWriter(filePath, append))
            {
                formatter.Serialize(writer, objectToWrite);
            }
        }

        public static void WriteToTextFile<T>(string directoryPath, string filePath, T objectToWrite, ITextFormatter formatter, bool append = false)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            WriteToTextFile(filePath, objectToWrite, formatter, append);
        }

        public static void WriteToTextFile<T>(string filePath, T objectToWrite, ITextFormatter formatter, ICryptoTransform encryptor, bool append = false)
        {
            using (var innerStream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                using (var cryptoStream = new CryptoStream(innerStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(cryptoStream))
                    {
                        formatter.Serialize(writer, objectToWrite);
                    }
                }
            }
        }

        public static void WriteToTextFile<T>(string directoryPath, string filePath, T objectToWrite, ITextFormatter formatter, ICryptoTransform encryptor, bool append = false)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            WriteToTextFile(filePath, objectToWrite, formatter, encryptor, append);
        }

        public static void WriteToTextFile<T>(string filePath, T objectToWrite, ITextFormatter<T> formatter, bool append = false)
        {
            using (var writer = new StreamWriter(filePath, append))
            {
                formatter.Serialize(writer, objectToWrite);
            }
        }

        public static void WriteToTextFile<T>(string directoryPath, string filePath, T objectToWrite, ITextFormatter<T> formatter, bool append = false)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            WriteToTextFile(filePath, objectToWrite, formatter, append);
        }

        public static void WriteToTextFile<T>(string filePath, T objectToWrite, ITextFormatter<T> formatter, ICryptoTransform encryptor, bool append = false)
        {
            using (var innerStream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                using (var cryptoStream = new CryptoStream(innerStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(cryptoStream))
                    {
                        formatter.Serialize(writer, objectToWrite);
                    }
                }
            }
        }

        public static void WriteToTextFile<T>(string directoryPath, string filePath, T objectToWrite, ITextFormatter<T> formatter, ICryptoTransform encryptor, bool append = false)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            WriteToTextFile(filePath, objectToWrite, formatter, encryptor, append);
        }

        /// <summary>
        /// Reads an object instance from a text file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the text file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <param name="formatter">The formatter to deserialize the text file.</param>
        /// <param name="default">The default value to return if the file cannot be read.</param>
        /// <returns>Returns a new instance of the object read from the text file.</returns>
        public static T ReadFromTextFile<T>(string filePath, ITextFormatter formatter, T @default = default)
        {
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    return formatter.Deserialize<T>(reader);
                }
            }

            return @default;
        }

        public static T ReadFromTextFile<T>(string directoryPath, string filePath, ITextFormatter formatter, T @default = default)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            return ReadFromTextFile(filePath, formatter, @default);
        }

        public static T ReadFromTextFile<T>(string filePath, ITextFormatter formatter, ICryptoTransform decryptor, T @default = default)
        {
            if (File.Exists(filePath))
            {
                using (var innerStream = File.Open(filePath, FileMode.Open))
                {
                    using (var cryptoStream = new CryptoStream(innerStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            return formatter.Deserialize<T>(reader);
                        }
                    }
                }
            }

            return @default;
        }

        public static T ReadFromTextFile<T>(string directoryPath, string filePath, ITextFormatter formatter, ICryptoTransform decryptor, T @default = default)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            return ReadFromTextFile(filePath, formatter, decryptor, @default);
        }

        public static T ReadFromTextFile<T>(string filePath, ITextFormatter<T> formatter, T @default = default)
        {
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    return formatter.Deserialize(reader);
                }
            }

            return @default;
        }

        public static T ReadFromTextFile<T>(string directoryPath, string filePath, ITextFormatter<T> formatter, T @default = default)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            return ReadFromTextFile(filePath, formatter, @default);
        }

        public static T ReadFromTextFile<T>(string filePath, ITextFormatter<T> formatter, ICryptoTransform decryptor, T @default = default)
        {
            if (File.Exists(filePath))
            {
                using (var innerStream = File.Open(filePath, FileMode.Open))
                {
                    using (var cryptoStream = new CryptoStream(innerStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            return formatter.Deserialize(reader);
                        }
                    }
                }
            }

            return @default;
        }

        public static T ReadFromTextFile<T>(string directoryPath, string filePath, ITextFormatter<T> formatter, ICryptoTransform decryptor, T @default = default)
        {
            if (!DirectoryExists(directoryPath))
                CreateDirectory(directoryPath);

            return ReadFromTextFile(filePath, formatter, decryptor, @default);
        }
    }
}