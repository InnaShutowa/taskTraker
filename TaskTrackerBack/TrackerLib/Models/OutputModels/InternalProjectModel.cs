using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackerLib;

namespace TrackerLib.Models {
    public class InternalProjectModel {

        public InternalProjectModel() { }
        public InternalProjectModel(TaskTrackerEntities db, Projects project) {
            ProjectId = project.ProjectId;
            ProjectName = project.Name;
            Description = project.Description;
            CreaterId = project.CreaterUserId;
            CreateDate = project.RegistrationDate;
        }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int CreaterId { get; set; }
        public DateTime? CreateDate { get; set; } = null;
    }
}