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
    public class TaskController : ApiController {
        //список задач для пользователя
        [HttpGet]
        public object Get(string apikey) {
            var result = ApiTaskManager.GetTaskList(apikey);
            return result;
        }

        [HttpPost]
        public object Post(ApiCreateTaskModel model) {
            var result = ApiTaskManager.CreateTask(model);
            return result;
        }


        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}