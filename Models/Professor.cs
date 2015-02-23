using System.Collections.Generic;

namespace Models
{
    public class Professor : Person
    {
        public IList<Course> Courses { get; set; }

        public Professor()
        {
            Role = Role.Professor;

            Courses = new List<Course>();
        }
    }
}
