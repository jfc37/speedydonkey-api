using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class UserEnroledBlocksModel
    {
        public IList<BlockModel> EnroledBlocks { get; set; }

        public IList<BlockModel> ConvertFromUser(User user, HttpRequestMessage request, IUrlConstructor urlConstructor, ICommonInterfaceCloner cloner)
        {
            if (user.EnroledBlocks == null)
                return new List<BlockModel>();
            return user.EnroledBlocks.Select(x => (BlockModel)new BlockModel().CloneFromEntity(request, urlConstructor, (Block)x, cloner))
                    .ToList();
        }
    }
}