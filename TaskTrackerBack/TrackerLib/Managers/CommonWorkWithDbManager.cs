using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLib.Enums;
using TrackerLib.Models;

namespace TrackerLib.Managers {
    /// <summary>
    /// менеджер, чтобы создавать элементарные сущности для бд
    /// </summary>
    public static class CommonWorkWithDbManager {
        /// <summary>
        /// добавляем инфу о новом проекте
        /// </summary>
        public static InternalResultModel CreateProjectModel(int userId,
                            string description,
                            string projectName) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var newProject = new Projects() {
                        CreaterUserId = userId,
                        Description = description,
                        Name = projectName,
                        IsDeleted = false,
                        RegistrationDate = DateTime.Now
                    };
                    db.Projects.Add(newProject);
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success, newProject);
                }
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// добавляем инфу о пользователе в проекте
        /// </summary>
        public static InternalResultModel CreateUserToProjectModel(int userId, 
                            int projectId, 
                            bool isOwner) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId);
                    if (user == null)
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var project = db.Projects.FirstOrDefault(a => a.ProjectId == projectId);
                    if (project == null)
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);

                    var newUserToProject = new UserToProjects() {
                        IsOwner = isOwner,
                        UserId = userId,
                        ProjectId = projectId
                    };
                    db.UserToProjects.Add(newUserToProject);
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success, newUserToProject);
                }
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// добавляем инфу по пользователю
        /// </summary>
        public static InternalResultModel CreateUserModel() {
            try {
                using (var db = new TaskTrackerEntities()) {

                }
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
            return null;
        }
        /// <summary>
        /// добавляем инфу о пользовательских авторизационных данных
        /// </summary>
        public static InternalResultModel CreateUserAuthModel() {
            try {
                using (var db = new TaskTrackerEntities()) {

                }
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
            return null;
        }
    }
}
