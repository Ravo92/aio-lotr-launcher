namespace PatchLauncher.Helper
{
    public class OptionIniParser
    {
        public readonly string fullPathOptionIniFile = Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME);

        public static void CreateDummyIniFile()
        {
            string appdataPath = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files")).ToString();

            if (!File.Exists(Path.Combine(appdataPath, ConstStrings.C_OPTIONSINI_FILENAME)))
            {
                File.Create(Path.Combine(appdataPath, ConstStrings.C_OPTIONSINI_FILENAME));
            }
        }

        public string ReadKey(string keyName)
        {
            StreamReader _streamReader = new(fullPathOptionIniFile);
            {
                string? keyValue = null;
                using (_streamReader)
                {
                    string inp;
                    while ((inp = _streamReader.ReadLine()) != null)
                    {
                        string[] parts = inp.Split(" = ");
                        if ((parts.Length == 2) && (parts[0] == keyName))
                        {
                            keyValue = parts[1];
                        }
                    }
                }
                return keyValue;
            }
        }

        public void WriteKey(string keyName, string keyValue)
        {
            StreamReader _streamReader = new(fullPathOptionIniFile);
            string inp;
            string changedFile;
            inp = _streamReader.ReadToEnd();
            _streamReader.Close();
            _streamReader.Dispose();

            if (!inp.Contains(keyName))
            {
                changedFile = inp += (keyName + " = " + keyValue + Environment.NewLine);
            }
            else
            {
                changedFile = inp.Replace(keyName + " = " + ReadKey(keyName), keyName + " = " + keyValue);
            }

            File.WriteAllText(fullPathOptionIniFile, changedFile);
        }
    }
}
