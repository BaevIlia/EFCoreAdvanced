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

            var courseResult = Course.FromId(favouriteCourseId);
            if (courseResult.IsFailure)
                return BadRequest(courseResult.Error);

            var student = new Student(nameResult.Value, courseResult.Value);

            var enrollment = new Enrollment(favouriteCourseGrade, courseResult.Value, student);

            student.Enrollments.Add(enrollment);

            _dbContext.Students.Add(student);

            _dbContext.SaveChangesAsync();

            return Ok("Ok");
        }
    }
}
