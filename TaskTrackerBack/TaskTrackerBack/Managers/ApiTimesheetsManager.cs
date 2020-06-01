using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskTrackerBack.Models.InputModels;
using TaskTrackerBack.Models.OutputModels;
using TrackerLib;
using TrackerLib.Enums;
using TrackerLib.Managers;
using TrackerLib.Models;
using TrackerLib.Models.InputModels;
using TrackerLib.Models.OutputModels;

namespace TaskTrackerBack.Managers {
    public static class ApiTimesheetsManager {
        /// <summary>
        /// создаем отчет по времени
        /// </summary>
        public static ResultModel CreateTimesheet(ApiCreateTimesheetModel model) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(model.Apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var user = CommonManager.GetUserDataByApikey(model.Apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(user);
                    }

                    var result = TimesheetManager
                        .CreateTimesheet(
                        new InternalCreateTimesheetModel(model.TaskId,
                        ((InternalUserModel)user.Data).UserId,
                        model.DateCreate,
                        model.Time));

                    return new ResultModel(result);
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// получаем список отчетов
        /// </summary>
        public static ResultModel GetTimesheetsList(string apikey, int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var user = CommonManager.GetUserDataByApikey(apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(user);
                    }

                    var result = TimesheetManager.GetTimesheets(userId);
                    if (result.StatusCode != StatusCodeEnum.Success)
                        return new ResultModel(result);

                    var data = new List<ApiOutputTimesheetsModel>();

                    if (result.Data != null ) {
                        ((List<InternalTimesheetModel>)result.Data).ForEach(a =>
                        {
                            data.Add(new ApiOutputTimesheetsModel(a));
                        });
                    }
                    return new ResultModel(data);
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// удаляем отчет
        /// </summary>
        public static ResultModel DeleteTimesheet(string apikey, int timesheetId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var user = CommonManager.GetUserDataByApikey(apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(user);
                    }

                    var result = TimesheetManager.DeleteTimesheet(timesheetId);
                    return new ResultModel(result);
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}