using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Codefire.Utilities;

namespace Codefire.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public class Encryption
    {
        #region [ Fields ]

        private readonly byte[] _keySalt = new byte[] { 0x42, 0x5E, 0x81, 0x2F, 0x95, 0x8C, 0x23, 0x1D };
        private readonly byte[] _ivSalt = new byte[] { 0x58, 0x4A, 0x95, 0x2E, 0x19, 0x6B, 0x76, 0x8F };

        private EncryptionType _encryptionType;
        
        #endregion

        #region [ Constuctors ]

        /// <summary>
        /// 
        /// </summary>
        private Encryption()
        {
            _encryptionType = EncryptionType.Rijndael;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptionType"></param>
        private Encryption(EncryptionType encryptionType)
        {
            _encryptionType = encryptionType;
        }

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private byte[] EncryptData(string key, byte[] inputData)
        {
            var outputStream = new MemoryStream();

            using (var cryptoStream = CreateEncryptor(key, outputStream))
            {
                cryptoStream.Write(inputData, 0, inputData.Length);
                cryptoStream.Close();

                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string EncryptData(string key, string inputString)
        {
            var inputData = ByteUtility.ToByteArray(inputString);
            var encryptData = EncryptData(key, inputData);

            return Convert.ToBase64String(encryptData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private byte[] DecryptData(string key, byte[] inputData)
        {
            var outputStream = new MemoryStream();
            
            using (var cryptoStream = CreateDecryptor(key, outputStream))
            {
                cryptoStream.Write(inputData, 0, inputData.Length);
                cryptoStream.Close();

                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string DecryptData(string key, string inputString)
        {
            var inputData = Convert.FromBase64String(inputString);
            var decryptData = DecryptData(key, inputData);

            return Encoding.UTF8.GetString(decryptData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CryptoStream CreateEncryptor(string key, MemoryStream outputStream)
        {
            var algorithm = CreateAlgorithm();
            CryptoStream stream = null;

            if (algorithm != null)
            {
                var keyData = GenerateCryptoKey(key, _keySalt, algorithm.KeySize / 8);
                var ivData = GenerateCryptoKey(key, _ivSalt, algorithm.BlockSize / 8);

                var encryptor = algorithm.CreateEncryptor(keyData, ivData);
                stream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write);
            }

            return stream;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CryptoStream CreateDecryptor(string key, MemoryStream outputStream)
        {
            var algorithm = CreateAlgorithm();
            CryptoStream stream = null;

            if (algorithm != null)
            {
                var keyData = GenerateCryptoKey(key, _keySalt, algorithm.KeySize / 8);
                var ivData = GenerateCryptoKey(key, _ivSalt, algorithm.BlockSize / 8);

                var decryptor = algorithm.CreateDecryptor(keyData, ivData);
                stream = new CryptoStream(outputStream, decryptor, CryptoStreamMode.Write);
            }

            return stream;
        }

        private SymmetricAlgorithm CreateAlgorithm()
        {
            switch (_encryptionType)
            {
                case EncryptionType.AES:
                    return Aes.Create();
                case EncryptionType.DES:
                    return DES.Create();
                case EncryptionType.RC2:
                    return RC2.Create();
                case EncryptionType.Rijndael:
                    return Rijndael.Create();
                case EncryptionType.TripleDES:
                    return TripleDES.Create();
                default:
                    return null;
            }
        }

        private byte[] GenerateCryptoKey(string value, byte[] salt, int size)
        {
            var crytoGenerator = new Rfc2898DeriveBytes(value, salt);
            return crytoGenerator.GetBytes(size);
        }

        #endregion

        #region [ Static Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string Encrypt(string key, string inputString)
        {
            return new Encryption().EncryptData(key, inputString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="inputString"></param>
        /// <param name="encryptionType"></param>
        /// <returns></returns>
        public static string Encrypt(string key, string inputString, EncryptionType encryptionType)
        {
            return new Encryption(encryptionType).EncryptData(key, inputString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string Decrypt(string key, string inputString)
        {
            return new Encryption().DecryptData(key, inputString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="inputString"></param>
        /// <param name="encryptionType"></param>
        /// <returns></returns>
        public static string Decrypt(string key, string inputString, EncryptionType encryptionType)
        {
            return new Encryption(encryptionType).DecryptData(key, inputString);
        }

        #endregion
    }
}