using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

namespace PatchLauncher.Classes
{
    internal static class ReadXMLFile
    {
       
        public static string GetXMLFileData()
        {
            using (var client = new HttpClient())
            {
                using (var s = client.GetStreamAsync("https://ravo92.github.io/PatchUpdate_BFME1.xml"))
                {
                    using (var fs = new FileStream("PatchUpdate_BFME1.xml", FileMode.OpenOrCreate))
                    {
                        s.Result.CopyTo(fs);
                    }
                }
            }

            string xmlFile = File.ReadAllText("PatchUpdate_BFME1.xml");
            XmlDocument xmldoc = new();
            xmldoc.LoadXml(xmlFile);

            XmlNodeList _urls = xmldoc.GetElementsByTagName("url");

            string returnStrings = string.Empty;

            foreach (XmlNode node in _urls)
            {
                returnStrings += node.InnerText;
            }

            return returnStrings;
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