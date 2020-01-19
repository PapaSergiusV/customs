using System;

namespace customs.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime Uploadtime { get; set; }
        public DateTime Killtime { get; set; }
        public bool Deleted { get; private set; }

        public File(string path, DateTime uploadtime, DateTime killtime, bool deleted)
        {
            Path = path;
            Uploadtime = uploadtime;
            Killtime = killtime;
            Deleted = deleted;
        }

        public File(string path, int lifetime)
        {
            Path = path;
            Uploadtime = DateTime.UtcNow;
            Killtime = DateTime.UtcNow.AddHours(lifetime);
        }
        public void Delete()
        {
            Deleted = true;
        }
    }
}
