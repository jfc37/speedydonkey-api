using System;
using System.Web.Http;
using ActionHandlers;
using SpeedyDonkeyApi.Extensions;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class ActionResultToCreatedHttpActionResult<T, TModel> : ActionResultToHttpActionResult<T, TModel>
    {
        public ActionResultToCreatedHttpActionResult(ActionReponse<T> actionResponse, Func<T, TModel> modelConvert, ApiController controller) 
            : base(actionResponse, modelConvert, controller)
        {
        }

        protected override IHttpActionResult GetSucessfulResult(ActionReponse<TModel> content)
        {
            return Controller.CreatedWithContent(content);
        }
    }
}
