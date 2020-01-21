using System;
using customs.Models;

namespace customs
{
    public class FileView
    {
        public int Id { get; }
        public string Name { get; }
        public string Path { get; }
        public string Lifetime { get; }
        public string Uploadtime { get; }
        public FileView(File file)
        {
            DateTime now = DateTime.UtcNow;
            Id = file.Id;
            Path = file.Path;
            Name = System.IO.Path.GetFileName(Path);
            Lifetime = file.Killtime < now ? "0" : (file.Killtime - now).ToString();
            Uploadtime = file.Uploadtime.ToString("h:mm tt dd.MM.yy ");
        }
    }
}
