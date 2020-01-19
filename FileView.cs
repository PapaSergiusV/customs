using System;

namespace customs
{
    public class FileView
    {
        public string Name { get; }
        public string Path { get; }
        public string Lifetime { get; }
        public string Uploadtime { get; }
        public FileView(string path, DateTime killtime, DateTime uploadtime)
        {
            DateTime now = DateTime.UtcNow;
            Path = path;
            Name = System.IO.Path.GetFileName(path);
            Lifetime = killtime < now ? "0" : (killtime - now).ToString();
            Uploadtime = uploadtime.ToString();
        }
    }
}
