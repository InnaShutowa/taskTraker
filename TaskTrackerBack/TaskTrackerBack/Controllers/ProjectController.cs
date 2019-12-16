using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskTrackerBack.Managers;
using TaskTrackerBack.Models.InputModels;

namespace TaskTrackerBack.Controllers {
    public class ProjectController : Controller {
        [HttpPost]
        public object Post(ApiCreateProjectModel model) {
            var result = ApiProjectManager.CreateProject(model);
            return null;
        }
        // GET: Project
        [HttpGet]
        public object Get() {
            return null;
        }
    }
}