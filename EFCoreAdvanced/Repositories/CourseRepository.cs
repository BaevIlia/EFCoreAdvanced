using CSharpFunctionalExtensions;
using EFCoreAdvanced.Database;
using EFCoreAdvanced.Entities;

namespace EFCoreAdvanced.Repositories
{
    public class CourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Course>> GetCourseById(long courseId) 
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course is null)
                return Result.Failure<Course>("Course not found");

            return course;
        }

        public void Save(Course course) 
        {
            _context.Courses.Attach(course);
        }
    }
}
