using System;
using System.Collections.Generic;
using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class LevelModel : ILevel, IApiModel<Level>
    {
        public string Name { get; set; }
        public IRoom Room { get; set; }
        public IList<ITeacher> Teachers { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ClassesInBlock { get; set; }
        public IList<IBlock> Blocks { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }
        public Level ToEntity()
        {
            return new Level
            {
                EndTime = EndTime,
                StartTime = StartTime,
                Name = Name,
                Id = Id,
                ClassesInBlock = ClassesInBlock
            };
        }

        public IApiModel<Level> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, Level entity)
        {
            return new LevelModel
            {
                ClassesInBlock = entity.ClassesInBlock,
                EndTime = entity.EndTime,
                Id = entity.Id,
                Name = entity.Name,
                StartTime = entity.StartTime,
                Url = urlConstructor.Construct("LevelApi", new { id = entity.Id}, request)
            };
        }

        public IApiModel<Level> CreateModelWithOnlyUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id)
        {
            throw new NotImplementedException();
        }
    }
}