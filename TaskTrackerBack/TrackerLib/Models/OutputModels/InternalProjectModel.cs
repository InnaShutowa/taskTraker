using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackerLib;
using TrackerLib.Models.OutputModels;

namespace TrackerLib.Models {
    public class InternalProjectModel {

        public InternalProjectModel() { }
        public InternalProjectModel(TaskTrackerEntities db, Projects project) {
            ProjectId = project.ProjectId;
            ProjectName = project.Name;
            Description = project.Description;
            CreaterId = project.CreaterUserId;
            CreateDate = project.RegistrationDate;

            var users = project
                .UserToProjects
                .Where(a => !a.UserProfiles.IsDeleted).ToList();
            var usersCount = users?
                .Select(a => a.UserId)?
                .Distinct()?.Count();
            var creater = db.UserProfiles.FirstOrDefault(a => a.UserId == project.CreaterUserId);

            CountUsers = usersCount ?? 0;
            CreaterFirstName = creater?.FirstName ?? "";
            CreaterSeconsName = creater?.LastName ?? "";

            var tasks = db.Tasks
                .Where(a => a.ProjectId == project.ProjectId && a.IsActive).ToList();

            TasksList = new List<InternalTaskModel>();
            tasks.ForEach(task =>
            {
                TasksList.Add(new InternalTaskModel(db, task));
            });

            UsersInProject = new List<UserPartModel>();
            users.ForEach(user =>
            {
                UsersInProject.Add(new UserPartModel(db, user.UserProfiles));
            });
        }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int CreaterId { get; set; }
        public DateTime? CreateDate { get; set; } = null;
        public int CountUsers { get; set; }
        public string CreaterFirstName { get; set; }
        public string CreaterSeconsName { get; set; }
        public List<InternalTaskModel> TasksList { get; set; }
        public List<UserPartModel> UsersInProject { get; set; }
    }

    public class UserPartModel {
        public UserPartModel() { }
        public UserPartModel(TaskTrackerEntities db, UserProfiles user) {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}