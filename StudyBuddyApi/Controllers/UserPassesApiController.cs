using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserPassesApiController : EntityPropertyApiController<CurrentUserPassesModel, PassModel, User>
    {
        private readonly ICommonInterfaceCloner _cloner;

        public UserPassesApiController(
            IRepository<User> entityRepository, 
            IUrlConstructor urlConstructor, 
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, urlConstructor, cloner, actionHandlerOverlord)
        {
            _cloner = cloner;
        }

        public HttpResponseMessage Post(int id, IList<PassModel> pass)
        {
            var userModel = new UserModel
            {
                Id = id,
                Passes = pass.Select(x => (IPass) x).ToList()
            };
            return PerformAction<AddPassToUser, UserModel, User>(userModel, x => new AddPassToUser(x));
        }
    }
    public class UserEnroledBlocksApiController : EntityPropertyApiController<UserEnroledBlocksModel, BlockModel, User>
    {
        public UserEnroledBlocksApiController(
            IRepository<User> entityRepository, 
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, urlConstructor, cloner, actionHandlerOverlord)
        {
        }
    }
    public class ClassRollApiController : EntityPropertyApiController<ClassRegisterModel, UserModel, Class>
    {
        public ClassRollApiController(
            IRepository<Class> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, urlConstructor, cloner, actionHandlerOverlord)
        {
        }
    }
    public class ClassAttendanceApiController : EntityPropertyApiController<ClassAttendanceModel, UserModel, Class>
    {
        public ClassAttendanceApiController(
            IRepository<Class> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
            : base(entityRepository, urlConstructor, cloner, actionHandlerOverlord)
        {
        }

        public HttpResponseMessage Post(int id, int studentId)
        {
            var classModel = new ClassModel
            {
                Id = id,
                ActualStudents = new List<IUser>
                {
                    new UserModel {Id = studentId}
                }
            };
            return PerformAction<CheckStudentIntoClass, ClassModel, Class>(classModel, x => new CheckStudentIntoClass(x));
        }

        public HttpResponseMessage Delete(int id, int studentId)
        {
            var classModel = new ClassModel
            {
                Id = id,
                ActualStudents = new List<IUser>
                {
                    new UserModel {Id = studentId}
                }
            };
            return PerformAction<RemoveStudentFromClass, ClassModel, Class>(classModel, x => new RemoveStudentFromClass(x));
        }
    }

    public abstract class EntityPropertyApiController<TViewModel, TModel, TEntity> : ApiController where TViewModel : IEntityView<TEntity, TModel>, new() where TEntity : class, IEntity
    {
        private readonly IRepository<TEntity> _entityRepository;
        private readonly IUrlConstructor _urlConstructor;
        private readonly ICommonInterfaceCloner _cloner;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        protected EntityPropertyApiController(
            IRepository<TEntity> entityRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner,
            IActionHandlerOverlord actionHandlerOverlord)
        {
            _entityRepository = entityRepository;
            _urlConstructor = urlConstructor;
            _cloner = cloner;
            _actionHandlerOverlord = actionHandlerOverlord;
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

        protected HttpResponseMessage PerformAction<TAction, TActionModel, TActionEntity>([FromBody]TActionModel model, Func<TActionEntity, TAction> actionCreator)
            where TAction : IAction<TActionEntity>
            where TActionModel : IApiModel<TActionEntity>, new()
            where TActionEntity : class, IEntity
        {
            var entity = model.ToEntity(_cloner);
            var action = actionCreator(entity);
            ActionReponse<TActionEntity> result = _actionHandlerOverlord.HandleAction<TAction, TActionEntity>(action);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<IApiModel<TActionEntity>>
                {
                    ActionResult = model.CloneFromEntity(Request, _urlConstructor, result.ActionResult, _cloner),
                    ValidationResult = result.ValidationResult
                });
        }
    }
}