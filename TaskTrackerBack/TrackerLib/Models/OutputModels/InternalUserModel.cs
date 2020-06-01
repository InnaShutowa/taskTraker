using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLib.Enums;
using TrackerLib.Models.OutputModels;

namespace TrackerLib.Models {
    /// <summary>
    /// модель данных по пользователю
    /// </summary>
    public class InternalUserModel {
        public InternalUserModel() { }

        public InternalUserModel(TaskTrackerEntities db, UserProfiles data) {
            if (data == null) return;
            UserId = data.UserId;
            Email = data.Email;
            Phone = data.Phone;
            BirthDate = data.BirthDate;
            IsAdmin = data.IsAdmin;
            FirstName = data.FirstName;
            LastName = data.LastName;
            FullName = data.FirstName + " " + data.LastName;
            Password = data.UserAuthData.PasswordHash;

            var tasks = db.Tasks.Where(a => a.UserId == data.UserId && a.IsActive).ToList();
            CountOpenTasks = tasks?.Count(a => a.TaskStatus != (int)TaskStatusEnum.Ready) ?? 0;
            CountClosedTasks = tasks?.Count(a => a.TaskStatus == (int)TaskStatusEnum.Ready) ?? 0;

            tasks.ForEach(task =>
            {
                TasksList.Add(new InternalTaskModel(db, task));
            });
            
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int CountOpenTasks { get; set; }
        public int CountClosedTasks { get; set; }
        public string Password { get; set; }
        public List<InternalTaskModel> TasksList { get; set; } = new List<InternalTaskModel>();
    }
}
