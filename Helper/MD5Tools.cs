using System.Security.Cryptography;

namespace Helper
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