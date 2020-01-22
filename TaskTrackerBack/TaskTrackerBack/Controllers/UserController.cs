using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaskTrackerBack.Managers;
using TaskTrackerBack.Models.InputModels;
using TrackerLib.Managers;

namespace TaskTrackerBack.Controllers {
    public class UserController : ApiController {
        // GET: User
        [HttpGet]
        public object Get() {
            return null;
        }

        [HttpPost]
        public object Post(ApiCreateUserAccModel model) {
            var result = ApiUserManager.CreateUserAcc(model);
            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}