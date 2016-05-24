namespace Contracts.Teachers
{
    /// <summary>
    /// The standard rates a teacher will be paid per event
    /// </summary>
    public class TeacherRateModel
    {
        public int Id { get; set; }
        public decimal SoloRate { get; set; }
        public decimal PartnerRate { get; set; }
        public string Name { get; set; }

        public TeacherRateModel()
        {
            
        }

        public TeacherRateModel(decimal soloRate, decimal partnerRate)
        {
            SoloRate = soloRate;
            PartnerRate = partnerRate;
        }

        public TeacherRateModel(decimal soloRate, decimal partnerRate, int id)
            : this(soloRate, partnerRate)
        {
            Id = id;
        }

        public TeacherRateModel(decimal soloRate, decimal partnerRate, int id, string name)
            : this(soloRate, partnerRate, id)
        {
            Name = name;
        }
    }
}