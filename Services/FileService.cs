using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace customs
{
    public class FileService : IFileService
    {
        private Context _db;

        public DbSet<Models.File> Files { get; }
        private string _path;
        private IWebHostEnvironment _appEnvironment;
        public FileService(Context context, IWebHostEnvironment appEnvironment)
        {
            _db = context;
            _path = "/files/";
            _appEnvironment = appEnvironment;
        }

        public Models.File[] All() => _db.Files.OrderBy(x => x.Killtime).ToArray();

        public Models.File Find(int id) => _db.Files.Find(id);

        public Models.File[] GetOutdated() => _db.Files.Where(x => x.Killtime < DateTime.UtcNow).ToArray();

        public void Save(IFormFile uploadedFile, int hours, int userId)
        {
            if (uploadedFile == null)
                return;
            string folder = $"{_appEnvironment.WebRootPath}{_path}{DateTime.UtcNow.ToFileTimeUtc().ToString()}";
            Directory.CreateDirectory(folder);
            string path = $"{folder}/{uploadedFile.FileName}";
            Models.File file = new Models.File(path, hours, userId);
            _db.Files.Add(file);
            _db.SaveChanges();
            FileStream fs = new FileStream(path, FileMode.Create);
            uploadedFile.CopyTo(fs);
            fs.Close();
        }

        public void Destroy(int id)
        {
            Models.File file = _db.Files.Find(id);
            File.Delete(file.Path);
            Directory.Delete(new FileInfo(file.Path).DirectoryName);
            _db.Files.Remove(file);
            _db.SaveChanges();
        }
    }
}
