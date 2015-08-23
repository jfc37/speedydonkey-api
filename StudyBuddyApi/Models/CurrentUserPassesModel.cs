﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class UserClaimsModel : IEntityView<User, string>
    {
        public IList<string> ConvertFromEntity(User user, HttpRequestMessage request)
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

        public IList<PassModel> ConvertFromEntity(User user, HttpRequestMessage request)
        {
            if (user.Passes == null)
                return new List<PassModel>();
            var validPasses = user.Passes.OfType<Pass>().Where(x => x.IsValid() || x.IsFuturePass());
            return
                validPasses.Select(x => (PassModel) new PassModel().CloneFromEntity(request, x))
                    .ToList();
        }
    }

    public interface IEntityView<TEntity, TModel>
    {
        IList<TModel> ConvertFromEntity(TEntity user, HttpRequestMessage request);
    }
}