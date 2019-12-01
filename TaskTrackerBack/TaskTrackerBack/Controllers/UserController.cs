using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TaskTrackerBack.Controllers {
    public class UserController : ApiController {
        // GET: User
        [HttpGet]
        public object Index() {
            return null;
        }
    }
}