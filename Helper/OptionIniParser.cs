using System.Text.RegularExpressions;

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
                    string importedOptionsFileText;
                    while ((importedOptionsFileText = _streamReader.ReadLine()) != null)
                    {
                        string[] parts = importedOptionsFileText.Split(" = ");
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
            string importedOptionsFileText;
            string changedOptionsFileText;
            importedOptionsFileText = _streamReader.ReadToEnd();
            _streamReader.Close();
            _streamReader.Dispose();

            if (!importedOptionsFileText.Contains(keyName))
            {
                changedOptionsFileText = importedOptionsFileText += (keyName + " = " + keyValue + Environment.NewLine);
            }
            else
            {
                changedOptionsFileText = importedOptionsFileText.Replace(keyName + " = " + ReadKey(keyName), keyName + " = " + keyValue + Environment.NewLine);
            }

            File.WriteAllText(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME), changedOptionsFileText);
        }

        public static void DeleteKey(string keyName)
        {
            StreamReader _streamReader = new(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME));
            string importedOptionsFileText;
            string changedOptionsFileText;
            importedOptionsFileText = _streamReader.ReadToEnd();
            _streamReader.Close();
            _streamReader.Dispose();

            if (importedOptionsFileText.Contains(keyName))
            {
                changedOptionsFileText = importedOptionsFileText.Replace(keyName + " = " + ReadKey(keyName), "");
            }
            else
            {
                return;
            }

            File.WriteAllText(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME), changedOptionsFileText);
        }

        public static void ClearOptionsFile()
        {
            StreamReader _streamReader = new(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME));
            string importedOptionsFileText = _streamReader.ReadToEnd();
            _streamReader.Close();
            _streamReader.Dispose();

            char[] lineFeedChars = { '\n', '\r' };
            string[] cleanStringArray = importedOptionsFileText.Split(lineFeedChars, StringSplitOptions.RemoveEmptyEntries);
            string cleanString = String.Join(Environment.NewLine, cleanStringArray);

            string changedOptionsFileText = cleanString + Environment.NewLine;

            File.WriteAllText(Path.Combine(fullPathOptionIniFile, ConstStrings.C_OPTIONSINI_FILENAME), changedOptionsFileText);
        }
    }
}
