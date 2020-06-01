using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TaskTrackerBack.Models.InputModels {
    public class ApiUserToProjectModel {
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }
    }
}