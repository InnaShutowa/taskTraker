using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaskTrackerBack.Managers;

namespace TaskTrackerBack.Controllers
{
    public class SingleTaskController : ApiController
    {
        // GET: SingleTask
        [HttpGet]
        public object Get(string apikey, int task_id)
        {
            var result = ApiTaskManager.GetTaskInfo(apikey, task_id);
            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}