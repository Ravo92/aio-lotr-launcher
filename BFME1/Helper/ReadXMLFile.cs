using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

namespace PatchLauncher.Helper
{
    internal static class ReadXMLFile
    {
       
        public static void GetXMLFileData()
        {
            using var client = new HttpClient();
            using var s = client.GetStreamAsync("https://ravo92.github.io/PatchUpdate_BFME1.xml");
            using var fs = new FileStream("PatchUpdate_BFME1.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            s.Result.CopyTo(fs);
        }

        public static int GetXMLFileVersion()
        {
            XElement response = XElement.Load("PatchUpdate_BFME1.xml");
            var status = response.Elements().Where(e => e.Name.LocalName == "version").Single().Value;

            int version = Convert.ToInt32(status);

            return version;
        }
    }
}