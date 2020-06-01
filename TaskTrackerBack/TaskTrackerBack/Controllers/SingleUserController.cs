using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaskTrackerBack.Managers;
using TaskTrackerBack.Models.InputModels;

namespace TaskTrackerBack.Controllers {
    public class SingleUserController : ApiController {
        // GET: SingleUser
        [HttpGet]
        public object Get(string apikey) {
            var result = ApiUserManager.GetUserInfoByApikey(apikey);
            return result;
        }

        [HttpPost]
        public object Post(ApiChangeTaskStatus model) {
            var result = ApiTaskManager.ChangeTaskStatus(model);
            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}