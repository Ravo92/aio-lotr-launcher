using System.Security.Cryptography;
using System;

namespace Helper
{
    public class MD5Tools
    {
        public static string CalculateMD5(string filename)
        {
            using var md5 = MD5.Create();
            if (File.Exists(filename))
            {
                using var stream = File.OpenRead(filename);
                var hash = md5.ComputeHash(stream);

                string md5hash = BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
                return md5hash;
            }
            else
            {
                return "404";
            }
        }

        public static async Task<string> CalculateMD5Async(string filename)
        {
            using MD5 md5 = MD5.Create();
            if (File.Exists(filename))
            {
                using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, 1024 * 1024 * 16, true);
                byte[] buffer = new byte[1024 * 1024 * 16];
                int bytesRead;
                do
                {
                    bytesRead = await stream.ReadAsync(buffer.AsMemory(0, 1024 * 1024 * 16));
                    if (bytesRead > 0)
                    {
                        md5.TransformBlock(buffer, 0, bytesRead, null, 0);
                    }
                } while (bytesRead > 0);

                md5.TransformFinalBlock(buffer, 0, 0);
                return BitConverter.ToString(md5.Hash!).Replace("-", "").ToUpperInvariant();
            }
            else
            {
                return "404";
            }
        }
    }
}