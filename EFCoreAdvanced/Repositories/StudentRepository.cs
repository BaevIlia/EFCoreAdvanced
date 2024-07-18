using CSharpFunctionalExtensions;
using EFCoreAdvanced.Database;
using EFCoreAdvanced.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAdvanced.Repositories
{
    public class StudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Student>> GetById(long studentId) 
        {
            var student = await _context.Students
              .Include(s => s.Enrollments)
              .ThenInclude(e => e.Course)
              .Include(s => s.FavouriteCourse)
              .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student is null)
                return Result.Failure<Student>("Student not found");

            return student;
        }

        public void Save(Student student) 
        {
            _context.Students.Attach(student);
        }
    }
}
