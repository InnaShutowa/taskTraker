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
    public static class ApiTaskManager {
        /// <summary>
        /// изменить статус задачи
        /// </summary>
        public static ResultModel ChangeTaskStatus(ApiChangeTaskStatus model) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(model.Apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var user = CommonManager.GetUserDataByApikey(model.Apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(user);
                    }

                    var result = TaskManager.ChangeStatusTask(model.TaskId, (TaskStatusEnum)model.Status, model.Date);
                    return new ResultModel(result);
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
            return null;
        }
        /// <summary>
        /// получаем информацию по конкретной задаче
        /// </summary>
        public static ResultModel GetTaskInfo(string apikey, int taskId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var user = CommonManager.GetUserDataByApikey(apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(user);
                    }

                    var result = TaskManager.GetTaskIntrnalInfo(taskId, ((InternalUserModel)user.Data).UserId);
                    if (result.StatusCode != StatusCodeEnum.Success) return new ResultModel(result);

                    var resultModel = new ApiOutputTaskModel((InternalTaskModel)result.Data);
                    return new ResultModel(resultModel);
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// создаем задачу
        /// </summary>
        public static ResultModel CreateTask(ApiCreateTaskModel model) {
            try {
                if (string.IsNullOrEmpty(model.Apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(model.Apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }
                if (!((InternalUserModel)user.Data).IsAdmin) {
                    return new ResultModel(StatusCodeEnum.NoRules);
                }

                if (DateTime.Now > model.DeadlineDate) {
                    return new ResultModel(StatusCodeEnum.InputDataIsWrong);
                }

                var result = TaskManager
                    .CreateTask(new InternalCreateTaskModel(
                        model.Title,
                        model.Description,
                        model.DeadlineDate,
                        model.Email,
                        model.ProjectId,
                        TaskStatusEnum.New,
                        ((InternalUserModel)user.Data).UserId));
                return new ResultModel(result);
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// поулчаем список задач
        /// </summary>
        public static ResultModel GetTaskList(string apikey) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var user = CommonManager.GetUserDataByApikey(apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(user);
                    }
                    var data = TaskManager.GetTaskList(((InternalUserModel)user.Data).UserId);
                    if (data.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(data);
                    }

                    var result = new ApiOutputResultModel((InterlalTaskListModel)data.Data);
                    return new ResultModel(result);
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// поулчаем список задач ДЛЯ КОНКРЕТНОГО ПОЛЬЗОВАТЕЛЯ ПО id
        /// </summary>
        public static ResultModel GetTaskListByUserId(string apikey, int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var admin = CommonManager.GetUserDataByApikey(apikey);
                    if (admin.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(admin);
                    }

                    var data = TaskManager.GetTaskList(userId);
                    if (data.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(data);
                    }

                    var result = new ApiOutputResultModel((InterlalTaskListModel)data.Data);
                    return new ResultModel(result);
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// удаляем задачу
        /// </summary>
        public static ResultModel DeleteTask(string apikey, int taskId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var admin = CommonManager.GetUserDataByApikey(apikey);
                    if (admin.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(admin);
                    }

                    var result = TaskManager.DeleteTask(taskId);
                    return new ResultModel(result);
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}