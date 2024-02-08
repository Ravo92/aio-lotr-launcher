using System.Security.Cryptography;

namespace Helper
{
    public class MD5Tools
    {
        public static async Task<string> CalculateMD5Async(string filename)
        {
            using var md5 = MD5.Create();
            if (File.Exists(filename))
            {
                using var stream = File.OpenRead(filename);
                var hash = await md5.ComputeHashAsync(stream);

                string md5hash = BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
                return md5hash;
            }
            else
            {
                return "404";
            }
        }
    }
}