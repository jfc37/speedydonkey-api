using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudyBuddyApi.Controllers
{
    public class TestApiController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse("testing!");
        }
    }
}
