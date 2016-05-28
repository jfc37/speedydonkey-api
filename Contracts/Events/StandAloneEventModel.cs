namespace Contracts.Events
{
    public class StandAloneEventModel : EventModel
    {
        public decimal Price { get; set; }
        public bool IsPrivate { get; set; }
        public decimal TeacherRate { get; set; }
    }
}