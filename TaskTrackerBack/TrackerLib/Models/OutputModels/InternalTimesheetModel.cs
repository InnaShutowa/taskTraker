using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLib.Models.OutputModels {
    public class InternalTimesheetModel {
        public InternalTimesheetModel() { }
        public InternalTimesheetModel(TaskTrackerEntities db, Timesheets timesheet) {
            TaskId = timesheet.TaskId;
            TaskName = timesheet.Tasks.Title;
            DateCreate = timesheet.Date;
            UserId = timesheet.UserId;
            ProjectId = timesheet.ProjectId;
            ProjectName = timesheet.Projects.Name;
            HoursCount = timesheet.Time;
            TimesheetId = timesheet.TimesheetId;
        }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime? DateCreate { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int HoursCount { get; set; }
        public int TimesheetId { get; set; }
    }
}
