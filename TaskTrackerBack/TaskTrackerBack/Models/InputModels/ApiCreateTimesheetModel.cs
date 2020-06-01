using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TaskTrackerBack.Models.InputModels {
    public class ApiChangeTaskStatus {
        [JsonProperty("apikey")]
        public string Apikey { get; set; } 
        [JsonProperty("task_id")]
        public int TaskId { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("date")]
        public DateTime? Date { get; set; }
    }
    public class ApiDeleteTimesheetModel {
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
        [JsonProperty("timesheet_id")]
        public int TimesheetId { get; set; }
    }
    public class ApiCreateTimesheetModel {
        [JsonProperty("task_id")]
        public int TaskId { get; set; }
        [JsonProperty("date_create")]
        public DateTime? DateCreate { get; set; }
        [JsonProperty("time")]
        public int Time { get; set; }
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
    }
}