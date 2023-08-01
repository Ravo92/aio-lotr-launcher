using System.Diagnostics;
using System.Reflection;
using System.Text;
using Helper;

namespace Restarter
{
    internal class Program
    {
        static readonly Mutex _mutex = new(true, ConstStrings.C_MUTEX_NAME_RESTARTER);
        static readonly string programPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        static void Main(string[] args)
        {
            try
            {
                if (_mutex.WaitOne(TimeSpan.Zero, true))
                {
                    Application.Run(new UpdaterDialog());

                    if (UpdateIsDownloaded.LauncherUpdateIsDownloaded)
                        return;
                }
                else
                {
                    Application.Exit();
                }

                if (args.Length == 0)
                {
                    if (GetLastSelectedGameLauncher() == 1)
                    {
                        StartBFME1Launcher("--official");
                    }

                    else if (GetLastSelectedGameLauncher() == 2)
                    {
                        StartBFME2Launcher("--official");
                    }

                    else if (GetLastSelectedGameLauncher() == 25)
                    {
                        StartBFME25Launcher("--official");
                    }
                }
                else
                {
                    switch (args[0])
                    {
                        case "--restart":

                            if (args[1] is not null)
                            {
                                if (args[1] == "--BFME1Launcher")
                                {
                                    StartBFME1Launcher("--official");
                                }

                                if (args[1] == "--BFME2Launcher")
                                {
                                    StartBFME2Launcher("--official");
                                }

                                if (args[1] == "--BFME25Launcher")
                                {
                                    StartBFME25Launcher("--official");
                                }
                            }
                            break;

                        case "--showLauncherUpdateLog":

                            if (GetLastSelectedGameLauncher() == 1)
                            {
                                StartBFME1Launcher("--showLauncherUpdateLog");
                            }

                            else if (GetLastSelectedGameLauncher() == 2)
                            {
                                StartBFME2Launcher("--showLauncherUpdateLog");
                            }

                            else if (GetLastSelectedGameLauncher() == 25)
                            {
                                StartBFME25Launcher("--showLauncherUpdateLog");
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }
        }

        private static void StartBFME1Launcher(string argument)
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME1_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = programPath;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.StartInfo.Arguments = argument;
                _restarterProcess.Start();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }
        }

        private static void StartBFME2Launcher(string argument)
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME2_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = programPath;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.StartInfo.Arguments = argument;
                _restarterProcess.Start();
            }
            catch (Exception ex)
            {
                LogHelper.LoggerRestarter.Error(ex, "");
            }
        }

        private static void StartBFME25Launcher(string argument)
        {
            try
            {
                Thread.Sleep(1000);
                Process _restarterProcess = new();
                _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME25_FILENAME;
                _restarterProcess.StartInfo.WorkingDirectory = programPath;
                _restarterProcess.StartInfo.UseShellExecute = true;
                _restarterProcess.StartInfo.Arguments = argument;
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