using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Codefire.Utilities;

namespace Codefire.Compression
{
    /// <summary>
    /// 
    /// </summary>
    public class Compressor
    {
        #region [ Fields ]

        private CompressType _compressType;
        
        #endregion

        #region [ Constuctors ]

        /// <summary>
        /// 
        /// </summary>
        private Compressor()
        {
            _compressType = CompressType.Deflate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressType"></param>
        private Compressor(CompressType compressType)
        {
            _compressType = compressType;
        }

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private byte[] InternalCompress(byte[] inputData)
        {
            if (inputData == null) return null;

            using (MemoryStream stream = new MemoryStream())
            {
                Stream compressStream = null;
                if (_compressType == CompressType.Deflate)
                {
                    compressStream = new DeflateStream(stream, CompressionMode.Compress);
                }
                else if (_compressType == CompressType.GZip)
                {
                    compressStream = new GZipStream(stream, CompressionMode.Compress);
                }
                else
                {
                    return inputData;
                }

                compressStream.Write(inputData, 0, inputData.Length);
                compressStream.Dispose();

                return stream.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private byte[] InternalCompress(string inputString)
        {
            byte[] inputData = ByteUtility.ToByteArray(inputString);
            byte[] hashData = Compress(inputData);

            return hashData;
        }

        #endregion

        #region [ Static Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static byte[] Compress(string inputString)
        {
            return new Compressor().InternalCompress(inputString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="compressType"></param>
        /// <returns></returns>
        public static byte[] Compress(string inputString, CompressType compressType)
        {
            return new Compressor(compressType).InternalCompress(inputString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] inputData)
        {
            return new Compressor().InternalCompress(inputData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="compressType"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] inputData, CompressType compressType)
        {
            return new Compressor(compressType).InternalCompress(inputData);
        }

        #endregion
    }
}