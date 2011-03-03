using System;
using System.Text;

namespace Codefire.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class ByteUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] inputData)
        {
            StringBuilder builder = new StringBuilder(inputData.Length * 2);
            foreach (byte inputByte in inputData)
            {
                builder.Append(inputByte.ToString("x2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(string inputString)
        {
            return UTF8Encoding.UTF8.GetBytes(inputString);
        }
    }
}