using System;
using System.Web.Http;
using ActionHandlers;
using Data.CodeChunks;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Extensions;

namespace SpeedyDonkeyApi.CodeChunks
{
    public abstract class ActionResultToHttpActionResult<T, TModel> : ICodeChunk<IHttpActionResult>
    {
        private readonly ActionReponse<T> _actionResponse;
        private readonly Func<T, TModel> _modelConvert;
        protected readonly ApiController Controller;

        protected ActionResultToHttpActionResult(ActionReponse<T> actionResponse, Func<T, TModel> modelConvert, ApiController controller)
        {
            _actionResponse = actionResponse;
            _modelConvert = modelConvert;
            Controller = controller;
        }

        public IHttpActionResult Do()
        {
            var modelActionResponse = new ActionReponse<TModel>(_modelConvert(_actionResponse.ActionResult),
                _actionResponse.ValidationResult);

            var response = modelActionResponse.ValidationResult.IsValid
                ? GetSucessfulResult(modelActionResponse)
                : Controller.BadRequestWithContent(modelActionResponse);

            return response;
        }

        protected abstract IHttpActionResult GetSucessfulResult(ActionReponse<TModel> content);
    }
}