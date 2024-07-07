namespace Example
{
    using Example.Model;
    using GraphQL.AspNet.Attributes;
    using GraphQL.AspNet.Controllers;

    public class ClassroomController : GraphController
    {
        [QueryRoot("classroom")]
        public Classroom RetrieveClassroom()
        {
            // assume a database call or some other operation
            // was made and no initial error was thrown
            // retrieving the object set
            return Classroom.MathClass;
        }
    }
}
