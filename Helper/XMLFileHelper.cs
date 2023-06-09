using System.Xml.Linq;

namespace Helper
{
    public static class XMLFileHelper
    {
        public const string C_XMLFile = "PatchUpdate_BFME1.xml";
        public const string C_XMLFileBeta = "PatchBetaUpdate_BFME1.xml";

        public static void GetXMLFileData(bool beta)
        {
            try
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
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ex.Message);
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
                XDocument _XDocument = XDocument.Load(C_XMLFileBeta);
                string result = (string)_XDocument.Descendants("patch222Pack").Elements("version").FirstOrDefault()!;
                int version = Convert.ToInt32(result);
                return version;
            }
            else
            {
                XDocument _XDocument = XDocument.Load(C_XMLFile);
                string result = (string)_XDocument.Descendants("patch222Pack").Elements("version").FirstOrDefault()!;
                int version = Convert.ToInt32(result);
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
            XDocument _XDocument = XDocument.Load(C_XMLFile);
            string result = (string)_XDocument.Descendants("mainPack").Elements("gameMainPackageURL").FirstOrDefault()!;
            return result;
        }

        /// <summary>
        /// Returns the game language packageURL from the downloaded XML file
        /// </summary>
        /// <param name="languageIsoCode"></param>
        /// <returns>The correct download URI corresponding to the <paramref name="languageIsoCode"/>.</returns>
        public static string GetXMLGameLanguagePackURL(string languageIsoCode)
        {
            XDocument _XDocument = XDocument.Load(C_XMLFile);
            string result = (string)_XDocument.Descendants("languagePacks").Descendants(languageIsoCode).Elements("url").FirstOrDefault()!;
            return result;
        }

        /// <summary>
        /// Returns the game language package MD5 from the downloaded XML file
        /// </summary>
        /// <param name="languageIsoCode"></param>
        /// <returns>The correct MD5 Hash Code corresponding to the <paramref name="languageIsoCode"/>.</returns>
        public static string GetXMLGameLanguageMD5Hash(string languageIsoCode)
        {
            XDocument _XDocument = XDocument.Load(C_XMLFile);
            string result = (string)_XDocument.Descendants("languagePacks").Descendants(languageIsoCode).Elements("md5").FirstOrDefault()!;
            return result;
        }
    }
}