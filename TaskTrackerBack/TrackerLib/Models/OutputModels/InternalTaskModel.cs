using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLib.Enums;

namespace TrackerLib.Models.OutputModels {
    public class InterlalTaskListModel {
        public InterlalTaskListModel() { }
        public InterlalTaskListModel(List<InternalTaskModel> tasks,
            List<InternalTaskModel> adminTasks) {
            TasksCreated = adminTasks;
            Tasks = tasks;
        }
        public InterlalTaskListModel(List<InternalTaskModel> tasks) {
            Tasks = tasks;
        }
        public List<InternalTaskModel> TasksCreated { get; set; } = new List<InternalTaskModel>();
        public List<InternalTaskModel> Tasks { get; set; } = new List<InternalTaskModel>();
    }

    public class InternalTaskModel {
        public InternalTaskModel() { }
        public InternalTaskModel(TaskTrackerEntities db, Tasks model) {
            TaskId = model.TaskId;
            TaskName = model.Title;
            TaskDescription = model.Description;
            DateCreate = model.DateCreate;
            DateDeadline = model.DeadlineDeta;
            DateFinished = model.DateFinished;

            var user = db.UserProfiles.FirstOrDefault(a => a.UserId == model.UserId);
            var creater = db.UserProfiles.FirstOrDefault(a => a.UserId == model.CreaterId);

            UserFirstName = user?.FirstName ?? "";
            UserLastName = user?.LastName ?? "";
            UserFullName = this.UserFirstName + " " + this.UserLastName;
            UserId = user?.UserId ?? 0;

            CreaterFirstName = creater?.FirstName ?? "";
            CreaterLastName = creater?.LastName ?? "";
            CreaterFullName = this.CreaterFirstName + " " + this.CreaterLastName;
            CreaterId = creater?.UserId ?? 0;

            var totalTime = model.Timesheets.Sum(a => a.Time);
            HoursCount = totalTime;
            TaskStatus = (TaskStatusEnum)model.TaskStatus;
        }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateDeadline { get; set; }
        public DateTime? DateFinished { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserFullName { get; set; }
        public int UserId { get; set; }
        public string CreaterFirstName { get; set; }
        public string CreaterLastName { get; set; }
        public string CreaterFullName { get; set; }
        public int CreaterId { get; set; }
        public int HoursCount { get; set; }
        public TaskStatusEnum TaskStatus { get; set; }
    }
}
