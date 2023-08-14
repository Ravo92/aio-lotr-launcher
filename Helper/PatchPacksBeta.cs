namespace Helper
{
    public class PatchPacksBeta
    {
        public int Version { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        
        public List<string> URLs { get; set; }
        public string MD5 { get; set; }
        public string FileName { get; set; }
    }
}