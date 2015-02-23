namespace Models
{
    public abstract class CourseWork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FinalMarkPercentage { get; set; }
        public GradeType GradeType { get; set; }
    }
}
