using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace customs
{
    public class UtilisationService : IHostedService, IDisposable
    {
        private Timer _timer;
        private Context _db;
        private readonly ILogger<UtilisationService> _logger;

        public UtilisationService(ILogger<UtilisationService> logger)
        {
            _logger = logger;
            _db = new Context();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Utilistation service is running");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        private void DoWork(object State)
        {
            _logger.LogInformation("Checking for outdated files");
            Models.File[] outdated = _db.Files.Where(x => x.Killtime < DateTime.UtcNow).ToArray();
            _logger.LogInformation($"{outdated.Length} outdated files found");
            if (outdated.Length == 0)
                return;
            foreach(Models.File f in outdated)
            {
                File.Delete(f.Path);
                Directory.Delete(new FileInfo(f.Path).DirectoryName);
            }
            _db.Files.RemoveRange(outdated);
            _db.SaveChangesAsync();
            _logger.LogInformation("Outdated files has been removed");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Utilistation service is stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
