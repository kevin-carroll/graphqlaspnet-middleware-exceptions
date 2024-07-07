namespace Example.Model
{
    using System.Collections.Generic;
    using GraphQL.AspNet.Attributes;

    public class Classroom
    {
        public static Classroom MathClass =
            new Classroom(1, "Math", new List<Student>
                {
                    new Student { Id = 1, Name = "John" },
                    new Student { Id = 2, Name = "Jane" }
                });



        private readonly List<Student> _students;

        public Classroom(int id, string name, List<Student> students)
        {
            this.Id = id;
            this.Name = name;
            _students = students ?? new List<Student>();

        }

        [GraphField]
        public List<Student> Students(bool? shouldThrowError = false)
        {

            // randomly simulate an error with extracting data from
            // "down stream" projections
            if (shouldThrowError.HasValue && shouldThrowError.Value)
                throw new PublicMessageException("Error Mapping Students!");

            return _students;

        }

        public int Id { get; }

        public string Name { get; }
    }
}
