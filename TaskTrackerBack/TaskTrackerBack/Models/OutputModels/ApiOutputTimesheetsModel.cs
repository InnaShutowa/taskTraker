using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrackerLib.Models.OutputModels;

namespace TaskTrackerBack.Models.OutputModels {
    public class ApiOutputTimesheetsModel {
        public ApiOutputTimesheetsModel() { }
        public ApiOutputTimesheetsModel(InternalTimesheetModel model) {
            TaskId = model.TaskId;
            TaskName = model.TaskName;
            DateCreate = model.DateCreate;
            UserId = model.UserId;
            ProjectId = model.ProjectId;
            ProjectName = model.ProjectName;
            HoursCount = model.HoursCount;
            TimesheetId = model.TimesheetId;
        }
        [JsonProperty("task_id")]
        public int TaskId { get; set; }
        [JsonProperty("task_name")]
        public string TaskName { get; set; }
        [JsonProperty("date_create")]
        public DateTime? DateCreate { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }
        [JsonProperty("project_name")]
        public string ProjectName { get; set; }
        [JsonProperty("hours_count")]
        public int HoursCount { get; set; }
        [JsonProperty("timesheet_id")]
        public int TimesheetId { get; set; }
    }
}