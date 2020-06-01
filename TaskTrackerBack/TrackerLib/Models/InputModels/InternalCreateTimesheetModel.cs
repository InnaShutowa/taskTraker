using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLib.Models.InputModels {
    public class InternalCreateTimesheetModel {
        public InternalCreateTimesheetModel() { }
        public InternalCreateTimesheetModel(int taskId,
            int userId,
            DateTime? date,
            int time) {
            TaskId = taskId;
            UserId = userId;
            DateCreate = date;
            Time = time;
        }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime? DateCreate { get; set; }
        public int Time { get; set; }
    }
}
