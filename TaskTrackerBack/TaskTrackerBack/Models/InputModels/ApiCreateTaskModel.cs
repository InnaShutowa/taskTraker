using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TaskTrackerBack.Models.InputModels {
    public class ApiDeleteTaskModel {
        public ApiDeleteTaskModel() { }
        public ApiDeleteTaskModel(string apikey, int taskId) {
            Apikey = apikey;
            TaskId = taskId;
        }
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
        [JsonProperty("task_id")]
        public int TaskId { get; set; }
    }
    public class ApiCreateTaskModel {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("deadline_date")]
        public DateTime DeadlineDate { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
    }
}