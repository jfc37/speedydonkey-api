using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserPassesApiController : EntityPropertyApiController<CurrentUserPassesModel, PassModel, User>
    {
        public UserPassesApiController(
            IRepository<User> entityRepository, 
            IUrlConstructor urlConstructor, 
            ICommonInterfaceCloner cloner) : base(entityRepository, urlConstructor, cloner)
        {
        }
    }
    public class UserEnroledBlocksApiController : EntityPropertyApiController<UserEnroledBlocksModel, BlockModel, User>
    {
        public UserEnroledBlocksApiController(
            IRepository<User> entityRepository, 
            IUrlConstructor urlConstructor, 
            ICommonInterfaceCloner cloner) : base(entityRepository, urlConstructor, cloner)
        {
        }
    }
    public class ClassRollApiController : EntityPropertyApiController<ClassRegisterModel, UserModel, Class>
    {
        public ClassRollApiController(
            IRepository<Class> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner)
            : base(entityRepository, urlConstructor, cloner)
        {
        }
    }

    public abstract class EntityPropertyApiController<TViewModel, TModel, TEntity> : ApiController where TViewModel : IEntityView<TEntity, TModel>, new() where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _entityRepository;
        private readonly IUrlConstructor _urlConstructor;
        private readonly ICommonInterfaceCloner _cloner;

        protected EntityPropertyApiController(
            IRepository<TEntity> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner)
        {
            _entityRepository = entityRepository;
            _urlConstructor = urlConstructor;
            _cloner = cloner;
        }

        public HttpResponseMessage Get(int id)
        {
            var entity = _entityRepository.Get(id);
            if (entity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var userScheduleModels = new TViewModel().ConvertFromEntity(entity, Request, _urlConstructor, _cloner);
            return userScheduleModels.Any()
                ? Request.CreateResponse(userScheduleModels)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}