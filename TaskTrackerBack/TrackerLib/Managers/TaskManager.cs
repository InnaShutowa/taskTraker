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
    public static class TaskManager {
        /// <summary>
        /// изменяем статус задачи
        /// </summary>
        public static InternalResultModel ChangeStatusTask(int taskId, TaskStatusEnum status, DateTime? dateFinished) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var task = db.Tasks.FirstOrDefault(a => a.IsActive && a.TaskId == taskId);
                    if (task == null)
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    task.TaskStatus = (int)status;
                    task.DateFinished = dateFinished;
                    if (status == TaskStatusEnum.New ||
                        status == TaskStatusEnum.Dev ||
                        status == TaskStatusEnum.QA) {
                        task.DateFinished = null;
                    }
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// получаем информацию по конкретной задаче
        /// </summary>
        public static InternalResultModel GetTaskIntrnalInfo(int taskId, int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId && !a.IsDeleted);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var task = db.Tasks.FirstOrDefault(a => a.IsActive && a.TaskId == taskId);
                    if (task == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);

                    var result = new InternalTaskModel(db, task);

                    return new InternalResultModel(StatusCodeEnum.Success, result);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// создаем задачу
        /// </summary>
        public static InternalResultModel CreateTask(InternalCreateTaskModel model) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var project = db.Projects
                        .FirstOrDefault(a => a.ProjectId == model.ProjectId && !a.IsDeleted);
                    if (project == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);

                    var user = db.UserProfiles.FirstOrDefault(a => a.Email == model.Email && !a.IsDeleted);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);

                    var userToProject = user.UserToProjects.FirstOrDefault(a => a.ProjectId == model.ProjectId);
                    if (userToProject == null) return new InternalResultModel(StatusCodeEnum.NoRules);
                    
                    var task = new Tasks()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        DateCreate = DateTime.Now,
                        DeadlineDeta = model.DeadlineDate,
                        UserId = user.UserId,
                        TaskStatus = model.Status,
                        ProjectId = model.ProjectId,
                        CreaterId = model.AdminId,
                        IsActive = true
                    };

                    db.Tasks.Add(task);
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// удаляем задачу
        /// </summary>
        public static InternalResultModel DeleteTask(int taskId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var task = db.Tasks.FirstOrDefault(a => a.TaskId == taskId && a.IsActive);
                    if (task == null)
                        return new InternalResultModel(StatusCodeEnum.DataAlreadyDeleted);

                    task.IsActive = false;
                    var timesheets = db.Timesheets.Where(a => a.TaskId == task.TaskId).ToList();
                    if (timesheets != null && timesheets.Count != 0) {
                        db.Timesheets.RemoveRange(timesheets);
                    }
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// получаем список задач
        /// </summary>
        public static InternalResultModel GetTaskList(int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId && !a.IsDeleted);
                    if (user == null)
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var tasks = new List<InternalTaskModel>();
                    var tasksAdmin = new List<InternalTaskModel>();
                    if (user.IsAdmin) {
                        var adminList = db.Tasks
                            .Where(a => a.IsActive && a.CreaterId == userId)
                            .ToList();
                        adminList.ForEach(elem =>
                        {
                            tasksAdmin.Add(new InternalTaskModel(db, elem));
                        });
                    }

                    var tasksList = db.Tasks
                        .Where(a => a.UserId == userId && a.IsActive)
                        .ToList();
                    tasksList.ForEach(elem =>
                    {
                        tasks.Add(new InternalTaskModel(db, elem));
                    });
                    var result = new InterlalTaskListModel(tasks, tasksAdmin);
                    return new InternalResultModel(StatusCodeEnum.Success, result);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}
