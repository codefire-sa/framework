using System;
using System.IO;
using Microsoft.Win32;

namespace Codefire.Utilities
{
    public static class FileUtility
    {
        public static string GetContentType(string fileName)
        {
            var contentType = "application/octetstream";
            var ext = Path.GetExtension(fileName).ToLower();

            var registryKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (registryKey != null && registryKey.GetValue("Content Type") != null)
            {
                contentType = registryKey.GetValue("Content Type").ToString();
            }
            
            return contentType;
        }
    }
}
