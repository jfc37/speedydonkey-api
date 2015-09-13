using System;
using System.Web.Http;
using ActionHandlers;
using Data.CodeChunks;
using SpeedyDonkeyApi.Controllers;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class ActionResultToHttpActionResult<T, TModel> : ICodeChunk<IHttpActionResult>
    {
        private readonly ActionReponse<T> _actionResponse;
        private readonly Func<T, TModel> _modelConvert;
        private readonly ApiController _controller;

        public ActionResultToHttpActionResult(ActionReponse<T> actionResponse, Func<T, TModel> modelConvert, ApiController controller)
        {
            _actionResponse = actionResponse;
            _modelConvert = modelConvert;
            _controller = controller;
        }

        public IHttpActionResult Do()
        {
            var modelActionResponse = new ActionReponse<TModel>(_modelConvert(_actionResponse.ActionResult),
                _actionResponse.ValidationResult);

            var response = modelActionResponse.ValidationResult.IsValid
                ? _controller.CreatedWithContent(modelActionResponse)
                : _controller.BadRequestWithContent(modelActionResponse);

            return response;
        }
    }
}
