using System.Diagnostics;
using System.Reflection;
using System.Text;
using Helper;

namespace Restarter
{
    internal class Program
    {
        static readonly string programPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    return;
                }

                Application.Run(new UpdaterDialog());

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
                        return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }
        }

        private static void StartBFME1Launcher()
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME1_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = programPath;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.StartInfo.Arguments = "--official";
                _restarterProcess.Start();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }
        }

        private static void StartBFME2Launcher()
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME2_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = programPath;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.StartInfo.Arguments = "--official";
                _restarterProcess.Start();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }
        }

        private static void StartBFME25Launcher()
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME25_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = programPath;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.StartInfo.Arguments = "--official";
                _restarterProcess.Start();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
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

                if (File.Exists(Path.Combine(programPath, ConstStrings.C_LAUNCHERSELECTEDINFOFILE)))
                {
                    selectedGame = Convert.ToInt32(File.ReadAllText(@Path.Combine(programPath, ConstStrings.C_LAUNCHERSELECTEDINFOFILE), Encoding.UTF8));
                }
                else
                {
                    using FileStream _fileStream = File.Create(Path.Combine(programPath, ConstStrings.C_LAUNCHERSELECTEDINFOFILE));
                    _fileStream.Write(selectedGameAsByteArray);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }

            return selectedGame;
        }
    }
}