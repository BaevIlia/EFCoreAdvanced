using EFCoreAdvanced.Database;
using EFCoreAdvanced.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPut("edit")]
        public async Task<ActionResult<string>> EditPersonalInfo(
            long studentId,
            string firstName,
            string lastName,
            long favouriteCourseId) 
        {
            var student = await _dbContext.Students.FindAsync(studentId);

            if (student is null)
                return BadRequest("Student not found");

            var nameResult = Name.Create(firstName, lastName);

            if (nameResult.IsFailure)
                return BadRequest(nameResult.Error);

         

            var course = await _dbContext.Courses.FindAsync(favouriteCourseId);

            if (course is null)
                return BadRequest("Course not found");

            student.Name = nameResult.Value;
            student.FavouriteCourse = course;

            await _dbContext.SaveChangesAsync();

            return Ok("Ok");
        }

        [HttpPut("enroll")]
        public async Task<ActionResult<string>> EnrollStudent(
            long studentId,
            long courseId,
            Grade grade) 
        {
            var student = await _dbContext.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .Include(s => s.FavouriteCourse)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student is null)
                return BadRequest("Student not found");


            var course = await _dbContext.Courses.FindAsync(courseId);

            if (course is null)
                return BadRequest("Course not found");

            var enrollment = new Enrollment(grade: grade, course: course, student: student);

            if (student.Enrollments.Any(e => e.Course == course))
            {
                return BadRequest("Enrollment already exists");

            }
            student.Enrollments.Add(enrollment);
            await _dbContext.SaveChangesAsync();

            return Ok("Ok");
        }
    }
}
