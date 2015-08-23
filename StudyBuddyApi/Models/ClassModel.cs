using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Models;
using Newtonsoft.Json;

namespace SpeedyDonkeyApi.Models
{
    public class ClassModel : ApiModel<Class, ClassModel>, IClass
    {
        public ClassModel()
        {
            
        }
        
        [JsonConstructor]
        public ClassModel(List<Teacher> teachers)
        {
            if (teachers != null)
                Teachers = teachers.OfType<ITeacher>().ToList();
        }
        protected virtual string RouteName
        {
            get { return "ClassApi"; }
        }

        public ICollection<ITeacher> Teachers { get; set; }
        public ICollection<IUser> RegisteredStudents { get; set; }
        public IBooking Booking { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public ICollection<IUser> ActualStudents { get; set; }
        public IBlock Block { get; set; }
        public ICollection<IPassStatistic> PassStatistics { get; set; }

        protected override void AddChildrenToEntity(Class entity)
        {
            if (ActualStudents != null)
            {
                entity.ActualStudents = ActualStudents
                    .Select(x => (IUser)((UserModel)x).ToEntity())
                    .ToList();
            }

            if (Teachers != null && Teachers.Any())
                entity.Teachers = Teachers;
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
            if (entity.Teachers != null)
            {
                model.Teachers = entity.Teachers.Select(x => (ITeacher)new TeacherModel
                {
                    Id = x.Id,
                    User = new UserModel
                    {

                        FirstName = x.User.FirstName,
                        Surname = x.User.Surname
                    }
                }).ToList();
            }
        }
    }
}