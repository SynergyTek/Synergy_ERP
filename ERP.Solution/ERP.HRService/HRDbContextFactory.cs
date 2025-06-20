using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ERP.HRService.Data;

namespace ERP.HRService
{
    public class HRDbContextFactory : IDesignTimeDbContextFactory<HRDbContext>
    {
        public HRDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HRDbContext>();
            // Use the same connection string as in your appsettings.json or environment
            optionsBuilder.UseNpgsql("Host=5.189.153.176;Port=6432;User ID=postgres;Password=Synergywelcome1;Database=ERP;Pooling=false;CommandTimeout=300;Timeout=300;");
            return new HRDbContext(optionsBuilder.Options);
        }
    }
} 