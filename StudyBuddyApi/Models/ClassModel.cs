using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class ClassModel : ApiModel<Class, ClassModel>, IClass
    {
        protected override string RouteName
        {
            get { return "ClassApi"; }
        }

        public IList<ITeacher> Teachers { get; set; }
        public ICollection<IUser> RegisteredStudents { get; set; }
        public IBooking Booking { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public ICollection<IUser> ActualStudents { get; set; }
        public IBlock Block { get; set; }
        public int NumberOfVisitors { get; set; }

        protected override void AddChildrenToEntity(Class entity, ICommonInterfaceCloner cloner)
        {
            if (ActualStudents != null)
            {
                entity.ActualStudents = ActualStudents
                    .Select(x => (IUser)((UserModel)x).ToEntity(cloner))
                    .ToList();
            }
        }

        protected override void AddChildrenToModel(Class entity, ClassModel model)
        {
            if (entity.Block != null)
            {
                model.Block = new BlockModel
                {
                    Id = entity.Block.Id
                };   
            }
        }
    }
}