using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class UserClaimsModel : IEntityView<User, string>
    {
        public IList<string> ConvertFromEntity(User user, HttpRequestMessage request, IUrlConstructor urlConstructor, ICommonInterfaceCloner cloner)
        {
            if (user.Claims == null)
                return new List<string>();
            return
                user.Claims.Split(',').ToList();
        }
    }
    public class CurrentUserPassesModel : IEntityView<User, PassModel>
    {
        public IList<PassModel> CurrentPasses { get; set; }

        public IList<PassModel> ConvertFromEntity(User user, HttpRequestMessage request, IUrlConstructor urlConstructor, ICommonInterfaceCloner cloner)
        {
            if (user.Passes == null)
                return new List<PassModel>();
            var validPasses = user.Passes.OfType<Pass>().Where(x => x.IsValid() || x.IsFuturePass());
            return
                validPasses.Select(x => (PassModel) new PassModel().CloneFromEntity(request, urlConstructor, x, cloner))
                    .ToList();
        }
    }

    public interface IEntityView<TEntity, TModel>
    {
        IList<TModel> ConvertFromEntity(TEntity user, HttpRequestMessage request, IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner);
    }
}