using CSharpFunctionalExtensions;

namespace EFCoreAdvanced.Entities
{
    public class Course : Entity 
    {
        public static readonly Course Math = new(1, "Math");
        public static readonly Course Chemistry = new(2, "Chemistry");

        public static readonly Course[] AllCourses = [Math, Chemistry];

        public string Name { get; }

        public static Result<Course> FromId(long id) 
        {
            var course = AllCourses.SingleOrDefault(x => x.Id == id);

            if (course is null) 
            {
                return Result.Failure<Course>("Course is not found");
            }
            return course;
        }

        private Course() { }
        private Course(long id, string name)
            : base(id) 
        {
            Name = name;
        }
    }
}


