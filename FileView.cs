using System;
using System.Text;
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
            Lifetime = file.Killtime < now ? "0" : SpanToString(file.Killtime - now);
            Uploadtime = file.Uploadtime.ToString("h:mm tt dd.MM.yy ");
        }

        private string SpanToString(in TimeSpan span)
        {
            string minutes = span.Minutes > 9 ? span.Minutes.ToString() : $"0{span.Minutes}";
            string seconds = span.Seconds > 9 ? span.Seconds.ToString() : $"0{span.Seconds}";
            return $"{span.Days} days {span.Hours}:{minutes}:{seconds}";
        }
    }
}
