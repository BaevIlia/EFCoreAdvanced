using CSharpFunctionalExtensions;

namespace EFCoreAdvanced.Entities
{
    public class Student : Entity
    {
        public Name Name { get; set; }

        public Course? FavouriteCourse { get; set; }

        public List<Enrollment> Enrollments { get; set; } = null;

        public Student(Name name, Course? favouriteCourse )
        {
            Name = name;
            FavouriteCourse = favouriteCourse;
        }
    }
}
