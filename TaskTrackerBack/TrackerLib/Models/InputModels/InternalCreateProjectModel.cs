using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLib.Models.InputModels {
    public class InternalCreateProjectModel {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int UserId { get; set; }
    }
}
