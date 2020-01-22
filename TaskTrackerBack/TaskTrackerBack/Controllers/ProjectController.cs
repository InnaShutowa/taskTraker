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
    public class ProjectController : ApiController {
        [HttpPost]
        public object Post(ApiCreateProjectModel model) {
            var result = ApiProjectManager.CreateProject(model);
            return result;
        }
        // GET: Project
        [HttpGet]
        public object Get(string apikey) {
            var result = ApiProjectManager.GetProjectsList(apikey);
            return null;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}