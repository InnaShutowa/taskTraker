using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TaskTrackerBack.Managers;
using TaskTrackerBack.Models.OutputModels;

namespace TaskTrackerBack.Controllers {
    public class StatisticController : ApiController {
        // GET: Statistic
        [HttpGet]
        public HttpResponseMessage Get(string apikey) {
            var result = ApiStatisticManager.GetStatistic(apikey);
            return result;
        }

        [HttpPost]
        public HttpResponseMessage Post(ApiPostFormModel model) {
            var result = ApiStatisticManager.GetForm(model.Apikey, 
                model.UserId, 
                model.Start, 
                model.Finish);

            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}