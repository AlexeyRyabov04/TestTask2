using Microsoft.EntityFrameworkCore;
using TestTask2.Server.Models;

namespace TestTask2.Server.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ReportFile> ReportFiles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
