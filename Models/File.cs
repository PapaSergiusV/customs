using System;

namespace customs.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime Uploadtime { get; set; }
        public DateTime Killtime { get; set; }
        public int UserId { get; set; }
        public User User { get; set ;}

        public File(string path, DateTime uploadtime, DateTime killtime, int userId)
        {
            Path = path;
            Uploadtime = uploadtime;
            Killtime = killtime;
            UserId = userId;
        }

        public File(string path, int lifetime)
        {
            Path = path;
            Uploadtime = DateTime.UtcNow;
            Killtime = DateTime.UtcNow.AddHours(lifetime);
        }
    }
}
