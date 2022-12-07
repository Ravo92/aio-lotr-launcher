using System.Security.Cryptography;

namespace PatchLauncher.Helper
{
    public class MD5Tools
    {
        static public string CalculateMD5(string filename)
        {
            using var md5 = MD5.Create();
            if (File.Exists(filename))
            {
                using var stream = File.OpenRead(filename);
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
            else
            {
                return "404";
            }
        }
    }
}