using SevenZipExtractor;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace PatchLauncher.Helper
{
    public class ZIPFileHelper
    {
        public ulong EntrySize = 0;
        public string EntryFilename = "";
        public int counter = 0;
        public int Percentage = 0;

        public Task ExtractArchive(string source, string destination, IProgress<ExtractionProgress> progress)
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
                    progress.Report(new ExtractionProgress() { Filename = EntryFilename, Count = counter, Max = archiveFile.Entries.Count });
                    entry.Extract(Path.Combine(destination, entry.FileName));
                }
            });
        }
    }
}