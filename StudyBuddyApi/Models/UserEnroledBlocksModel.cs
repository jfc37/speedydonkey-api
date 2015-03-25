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
            if (theClass.RegisteredStudents == null)
                return new List<UserModel>();

            return theClass.RegisteredStudents.Select(x => (UserModel)new UserModel().CloneFromEntity(request, urlConstructor, (User)x, cloner))
                    .ToList();
        }
    }
    public class ClassAttendanceModel : IEntityView<Class, UserModel>
    {
        public IList<UserModel> ConvertFromEntity(Class theClass, HttpRequestMessage request, IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner)
        {
            if (theClass.ActualStudents == null)
                return new List<UserModel>();

            return theClass.ActualStudents.Select(x => (UserModel)new UserModel().CloneFromEntity(request, urlConstructor, (User)x, cloner))
                    .ToList();
        }
    }
}