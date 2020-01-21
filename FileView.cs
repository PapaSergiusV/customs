using System;
using customs.Models;

namespace customs
{
    public class FileView
    {
        public string Link { get; }
        public string Name { get; }
        public string Path { get; }
        public string Lifetime { get; }
        public string Uploadtime { get; }
        public FileView(File file)
        {
            DateTime now = DateTime.UtcNow;
            Link = $"/Store/Download/{file.Id}";
            Path = file.Path;
            Name = System.IO.Path.GetFileName(Path);
            Lifetime = file.Killtime < now ? "0" : (file.Killtime - now).ToString();
            Uploadtime = file.Uploadtime.ToString();
        }
    }
}
