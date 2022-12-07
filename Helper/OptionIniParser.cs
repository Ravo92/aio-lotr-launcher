namespace PatchLauncher.Helper
{
    public class OptionIniParser
    {
        // TODO: Change the Game Directory detection Behaviours when game isnt installed yet, so every key defaults to "noValue"...
        public static readonly string fullPathOptionIniFile = Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "My Battle for Middle-earth Files")).ToString(); // Path.Combine(ConstStrings.GameAppdataFolderPath(), ConstStrings.C_OPTIONSINI_FILENAME);

        public static void CreateDummyIniFile()
        {
            Directory.CreateDirectory(fullPathOptionIniFile);

            if (!File.Exists(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME)))
            {
                File.Create(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME));
            }
        }

        public string ReadKey(string keyName)
        {
            StreamReader _streamReader = new(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME));
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
            StreamReader _streamReader = new(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME));
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

            File.WriteAllText(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME), changedFile);
        }
    }
}
