using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher.Classes
{
    internal class ZIPFileHelper
    {
        public static Task ExtractZIP(string source, string destination, IProgress<ExtractionProgress> progress)
        {
            return Task.Run(() =>
            {
                var sourceFullPath = Path.Combine(Application.StartupPath, "Download", source);
                SevenZipArchive archiveSystem = SevenZipArchive.Open(sourceFullPath);
                int counter = 0;

                foreach (var entry in archiveSystem.Entries)
                {
                    counter++;
                    entry.WriteToDirectory(destination, new ExtractionOptions()
                    {
                        ExtractFullPath = true,
                        Overwrite = true,
                        PreserveFileTime = true,
                        PreserveAttributes = true
                    });
                    progress.Report(new ExtractionProgress() { Filename = entry.ToString(), Count = counter, Max = archiveSystem.Entries.Count });
                }
            });
        }
    }
}
