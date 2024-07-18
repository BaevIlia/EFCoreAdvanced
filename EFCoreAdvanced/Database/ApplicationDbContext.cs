using EFCoreAdvanced.Configuration;
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

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"))
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Конфигурации отдельно
           // modelBuilder.ApplyConfiguration(new StudentConfiguration());

            //Все конфигурации из сборки
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


            modelBuilder.Entity<Course>(courseBuilder =>
            {
                courseBuilder.ToTable("Courses").HasKey(c => c.Id);
                courseBuilder.Property(c => c.Id).HasColumnName("CourseID");
                courseBuilder.Property(c => c.Name).HasMaxLength(100);

                courseBuilder.HasData(Course.Math, Course.Chemistry);
            });

            modelBuilder.Entity<Enrollment>(enrollmentBuilder =>
            {
                enrollmentBuilder.ToTable("Enrollments").HasKey(e => e.Id);
                enrollmentBuilder.Property(c => c.Id).HasColumnName("EnrollmentID");
                enrollmentBuilder.HasOne(s => s.Student).WithMany(e => e.Enrollments);
                enrollmentBuilder.HasOne(c => c.Course).WithMany();
                enrollmentBuilder.Property(e => e.Grade);


            });
        }

        public ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
