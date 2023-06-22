using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Helper;

namespace Restarter
{
    internal class Program
    {
        static readonly string configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
        static void Main(string[] args)
        {
            try
            {
                switch (args[0])
                {
                    case "--restart":

                        if (args[1] is not null)
                        {
                            if (args[1] == "--BFME1Launcher")
                            {
                                StartBFME1Launcher();
                            }

                            if (args[1] == "--BFME2Launcher")
                            {
                                StartBFME2Launcher();
                            }

                            if (args[1] == "--BFME25Launcher")
                            {
                                StartBFME25Launcher();
                            }
                        }
                        break;

                    case "--startLauncher":

                        if (GetLastSelectedGameLauncher() == 1)
                        {
                            StartBFME1Launcher();
                        }

                        else if (GetLastSelectedGameLauncher() == 2)
                        {
                            StartBFME2Launcher();
                        }

                        else if (GetLastSelectedGameLauncher() == 25)
                        {
                            StartBFME25Launcher();
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ConstStrings.LogTime + ex.ToString());
            }
        }

        private static void StartBFME1Launcher()
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME1_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.Start();
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ConstStrings.LogTime + ex.ToString());
            }
        }

        private static void StartBFME2Launcher()
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME2_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.Start();
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ConstStrings.LogTime + ex.ToString());
            }
        }

        private static void StartBFME25Launcher()
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME25_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.Start();
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ConstStrings.LogTime + ex.ToString());
            }
        }

        /// <summary>
        /// Selected Games:
        /// 1 = BFME1
        /// 2 = BFME2
        /// 25 = ROTWK
        /// </summary>
        /// <returns>Returns the selected Game Launcher as an integer value</returns>
        private static int GetLastSelectedGameLauncher()
        {
            int selectedGame = 1;

            try
            {
                byte[] selectedGameAsByteArray = new UTF8Encoding(true).GetBytes(selectedGame.ToString());

                if (File.Exists(Path.Combine(configPath, ConstStrings.C_LAUNCHERSELECTEDINFOFILE)))
                {
                    selectedGame = Convert.ToInt32(File.ReadAllText(@Path.Combine(configPath, ConstStrings.C_LAUNCHERSELECTEDINFOFILE), Encoding.UTF8));
                }
                else
                {
                    using FileStream _fileStream = File.Create(Path.Combine(configPath, ConstStrings.C_LAUNCHERSELECTEDINFOFILE));
                    _fileStream.Write(selectedGameAsByteArray);
                }
            }
            catch (Exception ex)
            {
                using StreamWriter file = new(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_ERRORLOGGING_FILE), append: true);
                file.WriteLineAsync(ConstStrings.LogTime + ConstStrings.LogTime + ex.ToString());
            }

            return selectedGame;
        }
    }
}