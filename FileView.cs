using System;
using System.Runtime.CompilerServices;
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

        private string SpanToString(in TimeSpan span) =>
            $"{span.Days} days {span.Hours}:{FormatTime(span.Minutes)}:{FormatTime(span.Seconds)}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string FormatTime(int time) =>
            time > 9 ? time.ToString() : $"0{time}";
    }
}
