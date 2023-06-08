using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

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

        /// <summary>
        /// Returns the main game-package URL from the downloaded XML file
        /// </summary>
        /// <param name="gameMainPackageURL"></param>
        /// <returns>The correct MD5 Hash Code corresponding to the <paramref name="gameMainPackageURL"/>.</returns>
        public static string GetXMLGameMainPackageURL()
        {
            XElement response = XElement.Load(C_XMLFile);
            var status = response.Elements().Where(e => e.Name.LocalName == "gameMainPackageURL").Single().Value;
            return status;
        }

        /// <summary>
        /// Returns the game language packageURL from the downloaded XML file
        /// </summary>
        /// <param name="languageIsoCode"></param>
        /// <returns>The correct download URI corresponding to the <paramref name="languageIsoCode"/>.</returns>
        public static string GetXMLGameLanguagePackURL(string languageIsoCode)
        {
            //XElement response = XElement.Load(C_XMLFile);
            //var status = response.Elements().Where(e => e.Name.LocalName == "languagePacks" + languageIsoCode + "url").Single().Value;
            //return status;

            XDocument systemXML = XDocument.Load(C_XMLFile);
            string result = (string)systemXML.Descendants("languagePacks").Descendants(languageIsoCode).Elements("url").FirstOrDefault()!;
            return result;
        }

        /// <summary>
        /// Returns the game language package MD5 from the downloaded XML file
        /// </summary>
        /// <param name="languageIsoCode"></param>
        /// <returns>The correct MD5 Hash Code corresponding to the <paramref name="languageIsoCode"/>.</returns>
        public static string GetXMLGameLanguageMD5Hash(string languageIsoCode)
        {
            //XElement response = XElement.Load(C_XMLFile);
            //var status = response.Elements().Where(e => e.Name.LocalName == "languagePacks" + languageIsoCode + "md5").Single().Value;
            //return status;

            XDocument systemXML = XDocument.Load(C_XMLFile);
            string result = (string)systemXML.Descendants("languagePacks").Descendants(languageIsoCode).Elements("md5").FirstOrDefault()!;
            return result;
        }
    }
}