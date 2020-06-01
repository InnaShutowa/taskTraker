using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaskTrackerBack.Managers;
using TaskTrackerBack.Models.InputModels;

namespace TaskTrackerBack.Controllers
{
    public class TaskJobsController : ApiController
    {
        // GET: TaskJobs
        [HttpGet]
        public object Get(string apikey, int user_id)
        {
            var result = ApiTaskManager.GetTaskListByUserId(apikey, user_id);
            return result;
        }

        [HttpPost]
        public object Post(ApiDeleteTaskModel model) {
            var result = ApiTaskManager.DeleteTask(model.Apikey, model.TaskId);
            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}