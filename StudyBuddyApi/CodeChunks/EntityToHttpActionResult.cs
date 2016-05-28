using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Common;
using Common.Extensions;
using Data.CodeChunks;
using SpeedyDonkeyApi.Extensions;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class EntityToHttpActionResult<T> : ICodeChunk<IHttpActionResult>
    {
        private readonly ApiController _controller;
        private readonly Option<T> _entity;
        private readonly Func<T, object> _modelConvert;

        public EntityToHttpActionResult(ApiController controller, Option<T> entity, Func<T, object> modelConvert)
        {
            _controller = controller;
            _entity = entity;
            _modelConvert = modelConvert;
        }

        public IHttpActionResult Do()
        {
            return _entity.IsEmpty()
                ? new NotFoundResult(_controller)
                : (IHttpActionResult)
                    new OkLoggableNegotiatedContentResult<object>(_modelConvert(_entity.Single()), _controller);
        }
    }
}