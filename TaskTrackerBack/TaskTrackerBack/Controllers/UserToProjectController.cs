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
    public class UserToProjectController : ApiController {
        // GET: UserNotInProject
        [HttpGet]
        public object Get(string apikey, int project_id) {
            var result = ApiProjectManager.GetUserToAdd(apikey, project_id);
            return result;
        }
        
        [HttpPost]
        public object Post(ApiUserToProjectModel model) {
            var result = ApiProjectManager.DeleteUserFromProject(model);
            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}