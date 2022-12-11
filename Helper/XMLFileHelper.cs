using System.Data;
using System.Xml.Linq;

namespace Helper
{
    public static class XMLFileHelper
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

            client.Dispose();
            s.Dispose();
            fs.Dispose();
        }

        public static int GetXMLFileVersion()
        {
            GetXMLFileData();
            XElement response = XElement.Load(C_XMLFile);
            var status = response.Elements().Where(e => e.Name.LocalName == "version").Single().Value;

            int version = Convert.ToInt32(status);

            return version;
        }
    }
}