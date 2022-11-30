namespace PatchLauncher.Helper
{
    internal class ExtractionProgress
    {
        public string? Filename { get; init; }
        public int Count { get; init; }
        public int Max { get; init; }
        public int Percentage {
            get
            {
                return 100 / Max * Count;
            }
        }
    }
}