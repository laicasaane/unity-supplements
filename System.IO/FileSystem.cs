using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace System.IO
{
    public static class FileSystem
    {
        private static BinaryFormatter _binaryFormatter;

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
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                _binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, ICryptoTransform encryptor, bool append = false)
        {
            using (Stream innerStream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                using (Stream cryptoStream = new CryptoStream(innerStream, encryptor, CryptoStreamMode.Write))
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
                using (Stream stream = File.Open(filePath, FileMode.Open))
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
                using (Stream innerStream = File.Open(filePath, FileMode.Open))
                {
                    using (Stream cryptoStream = new CryptoStream(innerStream, decryptor, CryptoStreamMode.Read))
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
    }
}