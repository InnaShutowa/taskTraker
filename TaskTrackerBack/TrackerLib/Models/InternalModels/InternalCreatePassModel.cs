using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLib.Models.InternalModels {

    public class InternalCreatePassModel {
        public InternalCreatePassModel() { }
        public InternalCreatePassModel(string pass, string hash) {
            Password = pass;
            PasswordHashMd5 = hash;
        }
        public string Password { get; set; }
        public string PasswordHashMd5 { get; set; }
    }
}
