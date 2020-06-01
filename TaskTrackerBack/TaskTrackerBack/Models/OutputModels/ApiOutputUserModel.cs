using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrackerLib.Models;

namespace TaskTrackerBack.Models.OutputModels {
    public class ApiOutputAuthUser {
        public ApiOutputAuthUser() { }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
    }
    public class ApiOutputUserModel {
        public ApiOutputUserModel() { }
        public ApiOutputUserModel(InternalUserModel model) {
            UserId = model.UserId;
            Email = model.Email;
            Phone = model.Phone;
            Email = model.Email;
            BirthDate = model.BirthDate;
            IsAdmin = model.IsAdmin;
            FirstName = model.FirstName;
            LastName = model.LastName;
            FullName = model.FullName;
            CountOpenTasks = model.CountOpenTasks;
            CountClosedTasks = model.CountClosedTasks;
            Password = model.Password;

            model.TasksList.ForEach(task =>
            {
                TasksList.Add(new ApiOutputTaskModel(task));
            });
        }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("birth_date")]
        public DateTime? BirthDate { get; set; }
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("count_open_tasks")]
        public int CountOpenTasks { get; set; }
        [JsonProperty("count_closed_tasks")]
        public int CountClosedTasks { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("tasks_list")]
        public List<ApiOutputTaskModel> TasksList = new List<ApiOutputTaskModel>();
    }
}