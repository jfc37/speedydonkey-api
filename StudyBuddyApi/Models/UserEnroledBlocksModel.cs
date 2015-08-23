using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class UserEnroledBlocksModel : IEntityView<User, BlockModel>
    {
        public IList<BlockModel> EnroledBlocks { get; set; }

        public IList<BlockModel> ConvertFromEntity(User user, HttpRequestMessage request)
        {
            if (user.EnroledBlocks == null)
                return new List<BlockModel>();
            return user.EnroledBlocks.Select(x => (BlockModel)new BlockModel().CloneFromEntity(request, (Block)x))
                    .ToList();
        }
    }
    public class ClassRegisterModel : IEntityView<Class, UserModel>
    {
        public IList<UserModel> ConvertFromEntity(Class theClass, HttpRequestMessage request)
        {
            var classRoll = new List<UserModel>();
            if (theClass.RegisteredStudents == null)
                return classRoll;

            foreach (var student in theClass.RegisteredStudents)
            {
                var studentModel = (UserModel)new UserModel().CloneFromEntity(request, (User)student);
                if (student.Passes != null)
                {
                    studentModel.Passes =
                        student.Passes.Where(x => x.IsValid())
                            .Select(x => (IPass)new PassModel().CloneFromEntity(request, (Pass)x))
                            .ToList();
                }
                classRoll.Add(studentModel);
            }
            return classRoll;
        }
    }
    public class ClassAttendanceModel : IEntityView<Class, UserModel>
    {
        public IList<UserModel> ConvertFromEntity(Class theClass, HttpRequestMessage request)
        {
            var attendance = new List<UserModel>();
            if (theClass.ActualStudents == null)
                return attendance;

            foreach (var student in theClass.ActualStudents)
            {
                var studentModel = (UserModel) new UserModel().CloneFromEntity(request, (User) student);
                if (student.Passes != null)
                {
                    studentModel.Passes =
                        student.Passes.Where(x => x.IsValid())
                            .Select(x => (IPass)new PassModel().CloneFromEntity(request, (Pass)x))
                            .ToList();   
                }
                attendance.Add(studentModel);
            }
            return attendance;
        }
    }
    public class ClassPassStaticsticsModel : IEntityView<Class, PassStatisticModel>
    {
        public IList<PassStatisticModel> ConvertFromEntity(Class theClass, HttpRequestMessage request)
        {
            var passStatistics = new List<PassStatisticModel>();
            if (theClass.PassStatistics == null)
                return passStatistics;

            foreach (var passStatistic in theClass.PassStatistics)
            {
                var passStatisticModel = (PassStatisticModel)new PassStatisticModel().CloneFromEntity(request, (PassStatistic)passStatistic);
                passStatistics.Add(passStatisticModel);
            }
            return passStatistics;
        }
    }
}