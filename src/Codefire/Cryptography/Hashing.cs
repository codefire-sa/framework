using System;
using System.Security.Cryptography;
using System.Text;
using Codefire.Utilities;

namespace Codefire.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public class Hashing
    {
        #region [ Fields ]

        private HashingType _hashingType;
        
        #endregion

        #region [ Constuctors ]

        /// <summary>
        /// 
        /// </summary>
        private Hashing()
        {
            _hashingType = HashingType.MD5;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashingType"></param>
        private Hashing(HashingType hashingType)
        {
            _hashingType = hashingType;
        }

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private byte[] ComputeHash(byte[] inputData)
        {
            HashAlgorithm algorithm = GetHashAlgorithm();

            return algorithm.ComputeHash(inputData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string ComputeHash(string inputString)
        {
            byte[] inputData = ByteUtility.ToByteArray(inputString);
            byte[] hashData = ComputeHash(inputData);

            return ByteUtility.ToHexString(hashData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private HashAlgorithm GetHashAlgorithm()
        {
            switch (_hashingType)
            {
                case HashingType.MD5:
                    return new MD5CryptoServiceProvider();
                case HashingType.SHA:
                    return new SHA1Managed();
                case HashingType.SHA256:
                    return new SHA256Managed();
                case HashingType.SHA384:
                    return new SHA384Managed();
                case HashingType.SHA512:
                    return new SHA512Managed();
                default:
                    return null;
            }
        }

        #endregion

        #region [ Static Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string Hash(string inputString)
        {
            return new Hashing().ComputeHash(inputString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="hashingType"></param>
        /// <returns></returns>
        public static string Hash(string inputString, HashingType hashingType)
        {
            return new Hashing(hashingType).ComputeHash(inputString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static byte[] Hash(byte[] inputData)
        {
            return new Hashing().ComputeHash(inputData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="hashingType"></param>
        /// <returns></returns>
        public static byte[] Hash(byte[] inputData, HashingType hashingType)
        {
            return new Hashing(hashingType).ComputeHash(inputData);
        }

        #endregion
    }
}