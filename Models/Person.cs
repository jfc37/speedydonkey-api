namespace Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Role Role { get; protected set; }

        public virtual User User { get; set; }
    }

    public enum Role
    {
        Student,
        Professor
    }
}
