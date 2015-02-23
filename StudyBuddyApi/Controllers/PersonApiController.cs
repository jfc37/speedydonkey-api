using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [BasicAuthAuthorise]
    public abstract class PersonApiController<TPerson, TModel> : BaseApiController
        where TPerson : Person
        where TModel : PersonModel
    {
        protected readonly IPersonRepository<TPerson> PersonRepository;
        protected readonly IActionHandlerOverlord ActionHandlerOverlord;
        protected readonly IModelFactory ModelFactory;
        private readonly IEntitySearch<TPerson> _entitySearch;

        protected PersonApiController(IPersonRepository<TPerson> personRepository, 
            IActionHandlerOverlord actionHandlerOverlord, 
            IModelFactory modelFactory,
            IEntitySearch<TPerson> entitySearch)
        {
            PersonRepository = personRepository;
            ActionHandlerOverlord = actionHandlerOverlord;
            ModelFactory = modelFactory;
            _entitySearch = entitySearch;
        }

        public HttpResponseMessage Get()
        {
            var people = (PersonRepository
                .GetAll()
                .Select(x => ModelFactory.ToModel(Request, x)) as IEnumerable<TModel>)
                .ToList();
            return people.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, people)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [BasicAuthAuthorise]
        public HttpResponseMessage Get(string q)
        {
            var matchingCourses = _entitySearch.Search(q);
            if (!matchingCourses.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK,
                matchingCourses.Select(x => ModelFactory.ToModel(Request, x)).ToList());
        }

        public HttpResponseMessage Get(int personId)
        {
            var person = ModelFactory.ToModel(Request, PersonRepository.Get(personId)) as TModel;
            return person != null
                ? Request.CreateResponse(HttpStatusCode.OK, person)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        protected HttpResponseMessage Post(int userId, [FromBody]TModel personModel)
        {
            if (personModel != null)
            {
                personModel.User = new UserModel { Id = userId };
            }
            var person = ModelFactory.Parse(personModel);
            var createPerson = new CreatePerson(person);
            ActionReponse<Person> result = ActionHandlerOverlord.HandleAction<CreatePerson, Person>(createPerson);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode,
                new ActionReponse<TModel>
                {
                    ValidationResult = result.ValidationResult,
                    ActionResult = (TModel)ModelFactory.ToModel(Request, result.ActionResult)
                });
        }

        public HttpResponseMessage Delete(int personId)
        {
            Person person = PersonRepository.Get(personId);
            if (person == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var deletePerson = new DeletePerson(person);
            var actionResult = ActionHandlerOverlord.HandleAction<DeletePerson, Person>(deletePerson);

            HttpStatusCode responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
