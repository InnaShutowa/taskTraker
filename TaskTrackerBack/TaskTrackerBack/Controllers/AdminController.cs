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
    public class AdminController : ApiController
    {
        // GET: Admin
        [HttpGet]
        public object Get(string apikey, int user_id) {
            var result = ApiUserManager.GetUserInfoById(apikey, user_id);
            return result;
        }

        [HttpPost]
        public object Post(ApiUserToProjectModel model) {
            var result = ApiProjectManager.AddUserToProject(model);
            return result;
        }

        [HttpOptions]
        public HttpResponseMessage Options() {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}