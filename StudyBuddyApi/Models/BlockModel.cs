using System;
using System.Collections.Generic;
using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class BlockModel : IBlock, IApiModel<Block>
    {
        public IList<IUser> EnroledStudents { get; set; }
        public ILevel Level { get; set; }
        public IList<IClass> Classes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }

        public Block ToEntity()
        {
            return new Block
            {
                Id = Id,
                Level = new Level
                {
                    Id = Level.Id
                }
            };
        }

        public IApiModel<Block> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, Block entity)
        {
            return new BlockModel
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Url = urlConstructor.Construct("BlockApi", new { id = entity.Id}, request)
            };
        }

        public IApiModel<Block> CreateModelWithOnlyUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}