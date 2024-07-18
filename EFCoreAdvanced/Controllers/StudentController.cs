using EFCoreAdvanced.Database;
using EFCoreAdvanced.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreAdvanced.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
                _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterStudent(
            string firstName,
            string lastName,
            long favouriteCourseId,
            Grade favouriteCourseGrade) 
        {
            var nameResult = Name.Create(firstName, lastName);
            if (nameResult.IsFailure)
                return BadRequest(nameResult.Error);

            var course = await _dbContext.Courses.FindAsync(favouriteCourseId);

           

            var student = new Student(nameResult.Value, course);

            var enrollment = new Enrollment(favouriteCourseGrade, course, student);

            if (course is null)
                return BadRequest();
            student.Enrollments.Add(enrollment);

           

            _dbContext.Students.Attach(student);

            var entries = _dbContext.ChangeTracker.Entries();

            await _dbContext.SaveChangesAsync();

            return Ok("Ok");
        }

       
    }
}
