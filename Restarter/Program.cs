using System.Diagnostics;
using Helper;

namespace Restarter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            switch (args[0])
            {
                case "--restart":

                    if (args[1] == "--BFME1Launcher")
                    {
                        //get the full location of the assembly with DaoTests in it
                        string fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                        //get the folder that's in
                        string theDirectory = Path.GetDirectoryName(fullPath)!;

                        Thread.Sleep(1000);
                        Process _restarterProcess = new();
                        _restarterProcess.StartInfo.FileName = ConstStrings.C_LAUNCHEREXE_BFME1_FILENAME;
                        //_restarterProcess.StartInfo.Arguments = "--restart";
                        _restarterProcess.StartInfo.WorkingDirectory = theDirectory;
                        _restarterProcess.StartInfo.UseShellExecute = true;
                        _restarterProcess.Start();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}