using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Data.CodeChunks;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class SetToHttpActionResult<T> : ICodeChunk<IHttpActionResult>
    {
        private readonly ApiController _controller;
        private readonly IEnumerable<T> _set;
        private readonly Func<T, object> _modelConvert;

        public SetToHttpActionResult(ApiController controller, IEnumerable<T> set, Func<T, object> modelConvert)
        {
            _controller = controller;
            _set = set;
            _modelConvert = modelConvert;
        }

        public IHttpActionResult Do()
        {
            return _set.Any()
                ? (IHttpActionResult)new OkNegotiatedContentResult<object>(_set.Select(_modelConvert), _controller)
                : new NotFoundResult(_controller);
        }
    }
}