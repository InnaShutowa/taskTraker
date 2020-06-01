using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLib.Enums;
using TrackerLib.Models;
using TrackerLib.Models.InputModels;
using TrackerLib.Models.OutputModels;

namespace TrackerLib.Managers {
    public static class TimesheetManager {
        /// <summary>
        /// создаем отчет
        /// </summary>
        public static InternalResultModel CreateTimesheet(InternalCreateTimesheetModel model) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == model.UserId && !a.IsDeleted);
                    var task = db.Tasks.FirstOrDefault(a => a.TaskId == model.TaskId && a.IsActive);

                    if (user == null ||
                        task == null) {
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    }

                    if (task.DateCreate > model.DateCreate) {
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    }

                    var timesheet = new Timesheets()
                    {
                        TaskId = model.TaskId,
                        ProjectId = task.ProjectId,
                        UserId = model.UserId,
                        Date = model.DateCreate,
                        Time = model.Time
                    };
                    db.Timesheets.Add(timesheet);
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// удаляем отчет
        /// </summary>
        public static InternalResultModel DeleteTimesheet(int timesheetId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var timesheet = db.Timesheets
                        .FirstOrDefault(a => a.TimesheetId == timesheetId);
                    if (timesheet == null)
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);

                    db.Timesheets.Remove(timesheet);
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// получаем список отчетов
        /// </summary>
        public static InternalResultModel GetTimesheets(int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles
                        .FirstOrDefault(a => a.UserId == userId && !a.IsDeleted);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var timesheets = db.Timesheets.Where(a => a.UserId == userId).ToList();
                    var result = new List<InternalTimesheetModel>();
                    timesheets.ForEach(a =>
                    {
                        result.Add(new InternalTimesheetModel(db, a));
                    });

                    return new InternalResultModel(StatusCodeEnum.Success, result);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}
