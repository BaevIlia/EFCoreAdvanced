using CSharpFunctionalExtensions;

namespace EFCoreAdvanced.Entities
{
    public class Enrollment : Entity
    {
        public Grade Grade { get;}

        public Course Course { get; }

        public Student Student { get;}

        private Enrollment() { }

        public Enrollment(Grade grade, Course course, Student student)
        {
            Grade = grade;
            Course = course;
            Student = student;
        }
    }
}
