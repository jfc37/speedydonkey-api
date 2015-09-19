using System;
using System.Web.Http;
using ActionHandlers;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Extensions;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class ActionResultToOkHttpActionResult<T, TModel> : ActionResultToHttpActionResult<T, TModel>
    {
        public ActionResultToOkHttpActionResult(ActionReponse<T> actionResponse, Func<T, TModel> modelConvert, ApiController controller) 
            : base(actionResponse, modelConvert, controller)
        {
        }

        protected override IHttpActionResult GetSucessfulResult(ActionReponse<TModel> content)
        {
            return Controller.OkWithContent(content);
        }
    }
}