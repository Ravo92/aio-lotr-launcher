namespace Helper
{
    public class OptionIniParser
    {
        // TODO: Change the Game Directory detection Behaviours when game isnt installed yet, so every key defaults to "noValue"...
        public static readonly string fullPathOptionIniFile = Directory.CreateDirectory(ConstStrings.GameAppdataFolderPath()).ToString();

        public static string ReadKey(string keyName)
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

        public static void WriteKey(string keyName, string keyValue)
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
