using System.Collections.Generic;
using System.Linq;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class UserEnroledBlocksModel : IEntityView<User, BlockModel>
    {
        public IEnumerable<BlockModel> ConvertFromEntity(User user)
        {
            if (user.EnroledBlocks == null)
                return new List<BlockModel>();
            return user.EnroledBlocks.Select(x => x.ToModel());
        }
    }
    public class ClassRegisterModel : IEntityView<Class, UserModel>
    {
        public IEnumerable<UserModel> ConvertFromEntity(Class theClass)
        {
            var classRoll = new List<UserModel>();
            if (theClass.RegisteredStudents == null)
                return classRoll;

            foreach (var student in theClass.RegisteredStudents)
            {
                student.Passes = student.Passes.Where(x => x.IsValid()).ToList();
                var studentModel = student.ToModel();
                classRoll.Add(studentModel);
            }
            return classRoll;
        }
    }
    public class ClassAttendanceModel : IEntityView<Class, UserModel>
    {
        public IEnumerable<UserModel> ConvertFromEntity(Class theClass)
        {
            var attendance = new List<UserModel>();
            if (theClass.ActualStudents == null)
                return attendance;

            foreach (var student in theClass.ActualStudents)
            {
                student.Passes = student.Passes.Where(x => x.IsValid()).ToList();
                var studentModel = student.ToModel();
                attendance.Add(studentModel);
            }
            return attendance;
        }
    }
    public class ClassPassStaticsticsModel : IEntityView<Class, PassStatisticModel>
    {
        public IEnumerable<PassStatisticModel> ConvertFromEntity(Class theClass)
        {
            var passStatistics = new List<PassStatisticModel>();
            if (theClass.PassStatistics == null)
                return passStatistics;

            foreach (var passStatistic in theClass.PassStatistics)
            {
                var passStatisticModel = passStatistic.ToModel();
                passStatistics.Add(passStatisticModel);
            }
            return passStatistics;
        }
    }
}