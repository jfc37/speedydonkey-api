using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class UserEnroledBlocksModel : IEntityView<User, BlockModel>
    {
        public IList<BlockModel> EnroledBlocks { get; set; }

        public IList<BlockModel> ConvertFromEntity(User user, HttpRequestMessage request, IUrlConstructor urlConstructor, ICommonInterfaceCloner cloner)
        {
            if (user.EnroledBlocks == null)
                return new List<BlockModel>();
            return user.EnroledBlocks.Select(x => (BlockModel)new BlockModel().CloneFromEntity(request, urlConstructor, (Block)x, cloner))
                    .ToList();
        }
    }
    public class ClassRegisterModel : IEntityView<Class, UserModel>
    {
        public IList<UserModel> ConvertFromEntity(Class theClass, HttpRequestMessage request, IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner)
        {
            var classRoll = new List<UserModel>();
            if (theClass.RegisteredStudents == null)
                return classRoll;

            foreach (var student in theClass.RegisteredStudents)
            {
                var studentModel = (UserModel)new UserModel().CloneFromEntity(request, urlConstructor, (User)student, cloner);
                if (student.Passes != null)
                {
                    studentModel.Passes =
                        student.Passes.Where(x => x.IsValid())
                            .Select(x => (IPass)new PassModel().CloneFromEntity(request, urlConstructor, (Pass)x, cloner))
                            .ToList();
                }
                classRoll.Add(studentModel);
            }
            return classRoll;
        }
    }
    public class ClassAttendanceModel : IEntityView<Class, UserModel>
    {
        public IList<UserModel> ConvertFromEntity(Class theClass, HttpRequestMessage request, IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner)
        {
            var attendance = new List<UserModel>();
            if (theClass.ActualStudents == null)
                return attendance;

            foreach (var student in theClass.ActualStudents)
            {
                var studentModel = (UserModel) new UserModel().CloneFromEntity(request, urlConstructor, (User) student, cloner);
                if (student.Passes != null)
                {
                    studentModel.Passes =
                        student.Passes.Where(x => x.IsValid())
                            .Select(x => (IPass)new PassModel().CloneFromEntity(request, urlConstructor, (Pass)x, cloner))
                            .ToList();   
                }
                attendance.Add(studentModel);
            }
            return attendance;
        }
    }
}