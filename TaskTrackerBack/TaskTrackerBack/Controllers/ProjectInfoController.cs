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
    public class ProjectInfoController : ApiController {
        // GET: ProjectInfo
        [HttpGet]
        public object Get(string apikey, int project_id) {
            var result = ApiProjectManager.GetProjectInfo(apikey, project_id);
            return result;
        }

        [HttpPost]
        public object Post(ApiGetProjectInfoModel model) {
            var result = ApiProjectManager.DeleteProject(model);
            return result;
        }

        [HttpDelete]
        public object Delete() {
            return null;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}