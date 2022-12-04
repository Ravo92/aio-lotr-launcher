using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PatchLauncher.Helper
{
    internal static class ReadXMLFile
    {
        public const string C_XMLFile = "PatchUpdate_BFME1.xml";
       
        public static void GetXMLFileData()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, C_XMLFile)))
            {
                File.Delete(Path.Combine(Application.StartupPath, C_XMLFile));
            }

            using var client = new HttpClient();
            using var s = client.GetStreamAsync("https://ravo92.github.io/PatchUpdate_BFME1.xml");

            using var fs = new FileStream(C_XMLFile, FileMode.CreateNew, FileAccess.ReadWrite);
            s.Result.CopyTo(fs);
        }

        public static int GetXMLFileVersion()
        {
            XElement response = XElement.Load(C_XMLFile);
            var status = response.Elements().Where(e => e.Name.LocalName == "version").Single().Value;

            int version = Convert.ToInt32(status);

            return version;
        }
    }
}