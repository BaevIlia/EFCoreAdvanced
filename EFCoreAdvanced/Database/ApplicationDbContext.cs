using EFCoreAdvanced.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAdvanced.Database
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"))
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(studentBuilder =>
            {
                studentBuilder.ToTable("Students").HasKey(s => s.Id);
                studentBuilder.Property(s=>s.Id).HasColumnName("StudentID");
          
            });
        }

        public ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
