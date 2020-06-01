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
    public class TimesheetJobsController : ApiController {
        // GET: TimesheetJobs
        [HttpPost]
        public object Post(ApiDeleteTimesheetModel model) {
            var result = ApiTimesheetsManager.DeleteTimesheet(model.Apikey, model.TimesheetId);
            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}