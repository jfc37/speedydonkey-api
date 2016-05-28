using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Class : Event
    {
        public Class() { }

        public Class(int id)
        {
            Id = id;
        }

        public virtual Block Block { get; set; }
        public virtual ICollection<PassStatistic> PassStatistics { get; set; }

        /// <summary>
        /// Gets a set of payslips dictating who should be paid how much for teaching this event
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<TeacherEventPaySlip> GetPaySlips()
        {
            var multipleTeachers = Teachers.Count > 1;

            return Teachers.Select(x => new TeacherEventPaySlip(x, GetRate(multipleTeachers, x)));
        }

        private static decimal GetRate(bool multipleTeachers, Teacher teacher)
        {
            return multipleTeachers ? teacher.Rate.PartnerRate : teacher.Rate.SoloRate;
        }
    }
}