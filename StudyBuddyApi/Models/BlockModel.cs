using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Models;
using Newtonsoft.Json;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class BlockModel : ApiModel<Block, BlockModel>, IBlock
    {
        public BlockModel() { }
        
        [JsonConstructor]
        public BlockModel(List<Teacher> teachers)
        {
            if (teachers != null)
                Teachers = teachers.ToList<ITeacher>();
        }
        public ICollection<ITeacher> Teachers { get; set; }
        public ICollection<IUser> EnroledStudents { get; set; }
        public ILevel Level { get; set; }
        public ICollection<IClass> Classes { get; set; }
        public ICollection<IAnnouncement> Announcements { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }


        protected override void AddChildrenToEntity(Block entity)
        {
            if (Level.IsNotNull())
                entity.Level = ((LevelModel) Level).ToEntity();

            if (Teachers.IsNotNull() && Teachers.Any())
                entity.Teachers = Teachers;
        }

        protected override void AddChildrenToModel(Block entity, BlockModel model)
        {
            if (entity.Teachers.IsNotNull())
            {
                model.Teachers = entity.Teachers.Select(x => new TeacherToTeacherModelMapping(x).Do()).ToList<ITeacher>();
            }
        }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
    }
}