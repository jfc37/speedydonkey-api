using System;

namespace Models
{
    /// <summary>
    /// The standard rates a teacher will be paid per event
    /// </summary>
    /// <seealso cref="Models.DatabaseEntity" />
    public class TeacherRate : DatabaseEntity
    {
        public virtual decimal SoloRate { get; set; }
        public virtual decimal PartnerRate { get; set; }

        public TeacherRate()
        {

        }

        public TeacherRate(decimal soloRate, decimal partnerRate)
        {
            SoloRate = soloRate;
            PartnerRate = partnerRate;

            CreatedDateTime = DateTime.Now;
        }
    }
}