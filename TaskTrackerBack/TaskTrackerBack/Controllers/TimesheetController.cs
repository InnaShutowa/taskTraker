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
    public class TimesheetController : ApiController
    {
        // GET: Timesheet
        [HttpGet]
        public object Get(string apikey, int user_id)
        {
            var result = ApiTimesheetsManager.GetTimesheetsList(apikey, user_id);
            return result;
        }
        [HttpPost]
        public object Post(ApiCreateTimesheetModel model) {
            var result = ApiTimesheetsManager.CreateTimesheet(model);
            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}