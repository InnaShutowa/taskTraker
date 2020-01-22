using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TaskTrackerBack.Controllers {
    public class AuthUserController : ApiController {
        // GET: AuthUser
        [HttpGet]
        public object Get() {
            return null;
        }

        [HttpPost]
        public object Post() {
            return null;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}