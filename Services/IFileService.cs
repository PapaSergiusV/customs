using customs.Models;
using Microsoft.AspNetCore.Http;

namespace customs
{
    public interface IFileService
    {
        File[] All();
        File Find(int id);
        File[] GetOutdated();
        void Save(IFormFile file, int hours, int userId);
        void Destroy(int id);
    }
}
