namespace LauncherGUI.Helpers
{
    public class ProgressHelper
    {
        public long TotalDownloadSizeInBytes { get; init; }
        public long ProgressedDownloadSizeInBytes { get; init; }
        public double DownloadSpeedSizeInBytes { get; init; }
        public double PercentageValue { get; init; }
        public string? CurrentFileName { get; init; }
        public int CurrentlyExtractedFileCount { get; init; }
        public int TotalArchiveFileCount { get; init; }
    }
}