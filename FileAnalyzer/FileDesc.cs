using System;

namespace FileAnalyzer
{
    [Serializable]
    public class FileDesc
    {
        public string name { get; set; }
        public string extension { get; set; }
        public string path { get; set; }
        public DateTime created { get; set; }
    }
}
