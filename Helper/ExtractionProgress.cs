namespace Helper
{
    public class ExtractionProgress
    {
        public string? Filename { get; init; }
        public int Count { get; init; }
        public int Max { get; init; }
        public int EntryPercentage { get; init; }
        public long TotalEntrySize { get; init; }
    }
}