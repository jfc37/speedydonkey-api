using System;
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
    public class ProfessorApiController : PersonApiController<Professor, PersonModel>
    {
        public ProfessorApiController(IPersonRepository<Professor> personRepository, 
            IActionHandlerOverlord actionHandlerOverlord, 
            IModelFactory modelFactory,
            IEntitySearch<Professor> entitySearch)
            : base(personRepository, actionHandlerOverlord, modelFactory, entitySearch)
        {
        }

        public HttpResponseMessage Post(int userId, ProfessorModel professorModel)
        {
            return base.Post(userId, professorModel);
        }

        public HttpResponseMessage Put(int personId, [FromBody]ProfessorModel professorModel)
        {
            professorModel.Id = personId;
            var professor = ModelFactory.Parse(professorModel);

            var updateProfessor = new UpdateProfessor(professor);
            ActionReponse<Professor> result = ActionHandlerOverlord.HandleAction<UpdateProfessor, Professor>(updateProfessor);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<ProfessorModel>
                {
                    ActionResult = ModelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }
    }
}
