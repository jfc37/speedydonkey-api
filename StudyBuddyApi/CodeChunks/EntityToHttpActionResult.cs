using System;
using System.Web.Http;
using System.Web.Http.Results;
using Common.Extensions;
using Data.CodeChunks;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class EntityToHttpActionResult<T> : ICodeChunk<IHttpActionResult>
    {
        private readonly ApiController _controller;
        private readonly T _entity;
        private readonly Func<T, object> _modelConvert;

        public EntityToHttpActionResult(ApiController controller, T entity, Func<T, object> modelConvert)
        {
            _controller = controller;
            _entity = entity;
            _modelConvert = modelConvert;
        }

        public IHttpActionResult Do()
        {
            return _entity.IsNotNull()
                ? (IHttpActionResult)new OkNegotiatedContentResult<object>(_modelConvert(_entity), _controller)
                : new NotFoundResult(_controller);
        }
    }
}