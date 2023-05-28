using System.Data;
using System.Xml.Linq;

namespace Helper
{
    public static class XMLFileHelper
    {
        public const string C_XMLFile = "PatchUpdate_BFME1.xml";
        public const string C_XMLFileBeta = "PatchBetaUpdate_BFME1.xml";

        public static void GetXMLFileData(bool beta)
        {
            if (beta)
            {
                if (File.Exists(Path.Combine(Application.StartupPath, C_XMLFileBeta)))
                {
                    File.Delete(Path.Combine(Application.StartupPath, C_XMLFileBeta));
                }

                using var client = new HttpClient();
                using var s = client.GetStreamAsync("https://dl.dropboxusercontent.com/s/3o12yb1lwf66sha/PatchBetaUpdate_BFME1.xml");

                using var fs = new FileStream(C_XMLFileBeta, FileMode.CreateNew, FileAccess.ReadWrite);
                s.Result.CopyTo(fs);

                client.Dispose();
                s.Dispose();
                fs.Dispose();
            }
            else
            {
                if (File.Exists(Path.Combine(Application.StartupPath, C_XMLFile)))
                {
                    File.Delete(Path.Combine(Application.StartupPath, C_XMLFile));
                }

                using var client = new HttpClient();
                using var s = client.GetStreamAsync("https://ravo92.github.io/PatchUpdate_BFME1.xml");

                using var fs = new FileStream(C_XMLFile, FileMode.CreateNew, FileAccess.ReadWrite);
                s.Result.CopyTo(fs);

                client.Dispose();
                s.Dispose();
                fs.Dispose();
            }
        }


        /// <summary>
        /// Returns the Patch version from the downloaded XML file
        /// </summary>
        /// <param name="beta"></param>
        /// <returns></returns>
        public static int GetXMLFileVersion(bool beta)
        {
            if (beta)
            {
                XElement response = XElement.Load(C_XMLFileBeta);
                var status = response.Elements().Where(e => e.Name.LocalName == "version").Single().Value;

                int version = Convert.ToInt32(status);

                return version;
            }
            else
            {
                XElement response = XElement.Load(C_XMLFile);
                var status = response.Elements().Where(e => e.Name.LocalName == "version").Single().Value;

                int version = Convert.ToInt32(status);

                return version;
            }
        }
    }
}