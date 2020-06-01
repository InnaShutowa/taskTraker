using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TrackerLib.Enums;
using TrackerLib.Models.OutputModels;

namespace TaskTrackerBack.Models.OutputModels {
    public class ApiOutputResultModel {
        public ApiOutputResultModel() { }

        public ApiOutputResultModel(InterlalTaskListModel model) {
            model
                .TasksCreated
                .ForEach(task =>
            {
                AdminTaskList.Add(new ApiOutputTaskModel(task));
            });

            model
                .Tasks
                .Where(a => a.TaskStatus == TaskStatusEnum.New)
                .ToList().ForEach(task =>
                {
                    NewTasks.Add(new ApiOutputTaskModel(task));
                });

            model
                .Tasks
                .Where(a => a.TaskStatus == TaskStatusEnum.Dev)
                .ToList().ForEach(task =>
                {
                    DevTasks.Add(new ApiOutputTaskModel(task));
                });

            model
                .Tasks
                .Where(a => a.TaskStatus == TaskStatusEnum.QA)
                .ToList().ForEach(task =>
                {
                    QaTasks.Add(new ApiOutputTaskModel(task));
                });

            model
               .Tasks
               .Where(a => a.TaskStatus == TaskStatusEnum.Ready)
               .ToList().ForEach(task =>
               {
                   ReadyTasks.Add(new ApiOutputTaskModel(task));
               });
        }

        public ApiOutputResultModel(List<ApiOutputTaskModel> newTasks,
            List<ApiOutputTaskModel> dev,
            List<ApiOutputTaskModel> qa,
            List<ApiOutputTaskModel> ready,
            List<ApiOutputTaskModel> adminList) {
            NewTasks = newTasks;
            DevTasks = dev;
            QaTasks = qa;
            ReadyTasks = ready;
            AdminTaskList = adminList;
        }

        [JsonProperty("admin_task_list")]
        public List<ApiOutputTaskModel> AdminTaskList { get; set; } = new List<ApiOutputTaskModel>();
        [JsonProperty("new_tasks")]
        public List<ApiOutputTaskModel> NewTasks { get; set; } = new List<ApiOutputTaskModel>();
        [JsonProperty("dev_tasks")]
        public List<ApiOutputTaskModel> DevTasks { get; set; } = new List<ApiOutputTaskModel>();
        [JsonProperty("qa_tasks")]
        public List<ApiOutputTaskModel> QaTasks { get; set; } = new List<ApiOutputTaskModel>();
        [JsonProperty("ready_tasks")]
        public List<ApiOutputTaskModel> ReadyTasks { get; set; } = new List<ApiOutputTaskModel>();
    }
    public class ApiOutputTaskModel {
        public ApiOutputTaskModel() { }
        public ApiOutputTaskModel(InternalTaskModel model) {
            TaskId = model.TaskId;
            TaskName = model.TaskName;
            DateCreate = model.DateCreate;
            DateDeadline = model.DateDeadline;
            DateFinished = model.DateFinished;
            UserFirstName = model.UserFirstName;
            UserLastName = model.UserLastName;
            UserFullName = model.UserFullName;
            UserId = model.UserId;
            CreaterFirstName = model.CreaterFirstName;
            CreaterLastName = model.CreaterLastName;
            CreaterFullName = model.CreaterFullName;
            CreaterId = model.CreaterId;
            HoursCount = model.HoursCount;
            TaskStatus = (int)model.TaskStatus;
            TaskDescription = model.TaskDescription;
            if (model.TaskStatus == TaskStatusEnum.New) TaskStatusText = "New";
            if (model.TaskStatus == TaskStatusEnum.Dev) TaskStatusText = "Develop";
            if (model.TaskStatus == TaskStatusEnum.QA) TaskStatusText = "QA";
            if (model.TaskStatus == TaskStatusEnum.Ready) TaskStatusText = "Ready";
        }
        [JsonProperty("task_id")]
        public int TaskId { get; set; }
        [JsonProperty("task_name")]
        public string TaskName { get; set; }
        [JsonProperty("task_description")]
        public string TaskDescription { get; set; }
        [JsonProperty("date_create")]
        public DateTime DateCreate { get; set; }
        [JsonProperty("date_deadline")]
        public DateTime? DateDeadline { get; set; }
        [JsonProperty("date_finished")]
        public DateTime? DateFinished { get; set; }
        [JsonProperty("user_first_name")]
        public string UserFirstName { get; set; }
        [JsonProperty("user_last_name")]
        public string UserLastName { get; set; }
        [JsonProperty("user_full_name")]
        public string UserFullName { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("create_first_name")]
        public string CreaterFirstName { get; set; }
        [JsonProperty("creater_last_name")]
        public string CreaterLastName { get; set; }
        [JsonProperty("creater_full_name")]
        public string CreaterFullName { get; set; }
        [JsonProperty("creater_id")]
        public int CreaterId { get; set; }
        [JsonProperty("hours_count")]
        public int HoursCount { get; set; }
        [JsonProperty("task_status")]
        public int TaskStatus { get; set; }
        [JsonProperty("task_status_text")]
        public string TaskStatusText { get; set; }
    }
}