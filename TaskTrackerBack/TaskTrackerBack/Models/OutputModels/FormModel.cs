using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrackerLib.Enums;
using TrackerLib.Models.OutputModels;

namespace TaskTrackerBack.Models.OutputModels {
    public class ApiPostFormModel {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("start")]
        public DateTime? Start { get; set; }
        [JsonProperty("finish")]
        public DateTime? Finish { get; set; }
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
    }
    public class FormCreateModel {
        public FormCreateModel() { }
        public FormCreateModel(List<InternalTaskModel> list) {
            if (list == null || list.Count == 0) return;
            list = list.OrderBy(a => a.DateCreate).ToList();
            FullName = list.First(a => a.UserFullName != null).UserFullName;
            Start = list.FirstOrDefault(a => a.DateCreate != null).DateCreate;
            Finish = DateTime.Now;
            TotalCount = list.Count;
            TotalNew = list.Count(a => a.TaskStatus == TaskStatusEnum.New);
            TotalDev = list.Count(a => a.TaskStatus == TaskStatusEnum.Dev);
            TotalQA = list.Count(a => a.TaskStatus == TaskStatusEnum.QA);
            TotalReady = list.Count(a => a.TaskStatus == TaskStatusEnum.Ready);
            CountOpened = list.Count(a => a.DateFinished == null);
            CountOvertime = list.Count(a => a.DateDeadline!= null && a.DateFinished > a.DateDeadline);
            CountClosedInTime = list.Count(a => a.DateDeadline == null || a.DateFinished <= a.DateDeadline);
            CountHours = list.Sum(a => a.HoursCount);
            UserStatisticForm = $"FORM REPORT FOR WORK BY SELECTED TIME PERIOD. \n" +
            $"Respected {this.FullName}! \n" +
            $"For the period {this.Start} to {this.Finish} \n " +
            $"you were assigned {this.TotalCount} tasks. \n" +
            $"Of these, {this.TotalNew} tasks are in the NEW stage, \n" +
            $"in the stage of DEV {this.TotalDev} tasks, \n" +
            $"in the stage of QA {this.TotalQA} tasks, \n" +
            $"in the stage of READY {this.TotalReady} tasks. \n" +
            $"Of them are closed during {this.CountClosedInTime} tasks, \n" +
            $"overdue {this.CountOvertime} tasks, \n" +
            $"{this.CountOpened} tasks are not closed. \n" +

            $"You spent {this.CountHours} hours in total on their implementation. \n" +
            $"Good job!";
        }

        public string FullName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int TotalCount { get; set; }
        public int TotalNew { get; set; }
        public int TotalDev { get; set; }
        public int TotalQA { get; set; }
        public int TotalReady { get; set; }
        public int CountOvertime { get; set; }
        public int CountOpened { get; set; }
        public int CountClosedInTime { get; set; }
        public int CountHours { get; set; }
        public string UserStatisticForm { get; set; } 
    }
    
    public class FormModel {
        public string UserStatisticForm { get; set; } = $"FORM REPORT FOR WORK BY SELECTED TIME PERIOD." +
            $"Respected {0}! " +
            $"For the period {1} to {2} you were assigned {3} tasks." +
            $"Of these, {4} tasks are in the NEW stage," +
            $"in the stage of DEV {5} tasks," +
            $"in the stage of QA {6} tasks, " +
            $"in the stage of READY {7} tasks." +
            $"Of them are closed during {8} tasks," +
            $"overdue {9} tasks," +
            $"{10} tasks are not closed." +

            $"You spent {11} hours in total on their implementation." +
            $"Good job!";
    }
}