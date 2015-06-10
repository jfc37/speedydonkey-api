using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Models;
using Newtonsoft.Json;

namespace SpeedyDonkeyApi.Models
{
    public class BlockModel : ApiModel<Block, BlockModel>, IBlock
    {
        public BlockModel()
        {
            
        }
        
        [JsonConstructor]
        public BlockModel(List<Teacher> teachers)
        {
            if (teachers != null)
                Teachers = teachers.OfType<ITeacher>().ToList();
        }
        public ICollection<ITeacher> Teachers { get; set; }
        public ICollection<IUser> EnroledStudents { get; set; }
        public ILevel Level { get; set; }
        public ICollection<IClass> Classes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }


        protected override string RouteName
        {
            get { return "BlockApi"; }
        }

        protected override void AddChildrenToEntity(Block entity, ICommonInterfaceCloner cloner)
        {
            if (Level != null)
                entity.Level = ((LevelModel) Level).ToEntity(cloner);

            if (Teachers != null && Teachers.Any())
                entity.Teachers = Teachers;
        }

        protected override void AddChildrenToModel(Block entity, BlockModel model)
        {
            if (entity.Teachers != null)
            {
                model.Teachers = entity.Teachers.Select(x => (ITeacher)new TeacherModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname
                }).ToList();
            }
        }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
    }
}