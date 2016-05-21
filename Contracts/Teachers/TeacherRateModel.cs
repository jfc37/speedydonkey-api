namespace Contracts.Teachers
{
    /// <summary>
    /// The standard rates a teacher will be paid per event
    /// </summary>
    public class TeacherRateModel
    {
        public decimal SoloRate { get; set; }
        public decimal PartnerRate { get; set; }

        public TeacherRateModel()
        {
            
        }

        public TeacherRateModel(decimal soloRate, decimal partnerRate)
        {
            SoloRate = soloRate;
            PartnerRate = partnerRate;
        }
    }
}