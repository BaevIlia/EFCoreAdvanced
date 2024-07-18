using EFCoreAdvanced.Database;
using EFCoreAdvanced.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAdvanced
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddScoped<StudentRepository>();
            builder.Services.AddScoped<CourseRepository>();
               

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) 
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();
            app.Run();
        }
    }
}