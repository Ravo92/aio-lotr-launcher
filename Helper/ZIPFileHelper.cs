using Serilog.Core;
using Serilog;
using SevenZipExtractor;

namespace Helper
{
    public class ZIPFileHelper
    {
        public ulong EntrySize = 0;
        public string EntryFilename = "";
        public int counter = 0;
        public int Percentage = 0;

        public Task? ExtractArchive(string source, string destination, IProgress<ProgressHelper> progress)
        {
            try
            {
                return Task.Run(() =>
                {
                    using ArchiveFile archiveFile = new(Path.Combine(Application.StartupPath, source));

                    foreach (Entry entry in archiveFile.Entries)
                    {
                        counter++;
                        EntrySize = entry.Size;
                        // extract to file
                        EntryFilename = entry.FileName;
                        progress.Report(new ProgressHelper() { Filename = EntryFilename, Count = counter, Max = archiveFile.Entries.Count });
                        entry.Extract(Path.Combine(destination, entry.FileName));
                    }
                });
            }
            catch (Exception ex)
            {
                Logger _log = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File(Path.Combine(ConstStrings.C_LOGFOLDER_NAME, ConstStrings.C_LOGFILE_ZIPFILE_NAME)).CreateLogger();
                _log.Error(ex.ToString());
                return null;
            }
        }
    }
}