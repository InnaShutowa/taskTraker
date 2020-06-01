using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLib.Enums;

namespace TrackerLib.Models.InputModels {
    public class InternalCreateTaskModel {
        public InternalCreateTaskModel() { }
        public InternalCreateTaskModel(string title,
            string descr,
            DateTime deadline,
            string email,
            int projectId,
            TaskStatusEnum status,
            int adminId) {
            Title = title;
            Description = descr;
            DeadlineDate = deadline;
            Email = email;
            ProjectId = projectId;
            Status = (int)status;
            AdminId = adminId;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string Email { get; set; }
        public int ProjectId { get; set; }
        public int Status { get; set; }
        public int AdminId { get; set; }
    }
}
