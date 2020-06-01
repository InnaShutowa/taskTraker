using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrackerLib.Models;

namespace TaskTrackerBack.Models.OutputModels {
    public class ApiOutputProjectModel {
        public ApiOutputProjectModel() { }
        public ApiOutputProjectModel(InternalProjectModel model) {
            ProjectId = model.ProjectId;
            ProjectName = model.ProjectName;
            Description = model.Description;
            CreaterId = model.CreaterId;
            CreateDate = model.CreateDate;
            CountUsers = model.CountUsers;
            CreaterFirstName = model.CreaterFirstName;
            CreaterLastName = model.CreaterSeconsName;
            TasksList = new List<ApiOutputTaskModel>();
            model.TasksList.ForEach(tsk =>
            {
                TasksList.Add(new ApiOutputTaskModel(tsk));
            });
            UsersList = new List<ApiOutUserPartModel>();
            model.UsersInProject.ForEach(a =>
            {
                UsersList.Add(new ApiOutUserPartModel(a));
            });
        }
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }
        [JsonProperty("project_name")]
        public string ProjectName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("creater_id")]
        public int CreaterId { get; set; }
        [JsonProperty("create_date")]
        public DateTime? CreateDate { get; set; } = null;
        [JsonProperty("count_users")]
        public int CountUsers { get; set; }
        [JsonProperty("creater_first_name")]
        public string CreaterFirstName { get; set; }
        [JsonProperty("creater_last_name")]
        public string CreaterLastName { get; set; }
        [JsonProperty("tasks_list")]
        public List<ApiOutputTaskModel> TasksList { get; set; }
        [JsonProperty("users_list")]
        public List<ApiOutUserPartModel> UsersList { get; set; }
    }

    public class ApiOutUserPartModel {
        public ApiOutUserPartModel() { }
        public ApiOutUserPartModel(UserPartModel user) {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            FullName = user.FirstName + " " + user.LastName;
        }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }

}