using System;
using System.Collections.Generic;
using System.Linq;
using TrackerLib.Enums;
using TrackerLib.Models;
using TrackerLib.Models.InputModels;

namespace TrackerLib.Managers {
    public static class ProjectManager {
        /// <summary>
        /// получаем список активных пользователей, которых можно добавить в проект
        /// </summary>
        /// <returns></returns>
        public static InternalResultModel GetUserToAddInProjectList(int projectId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var project = db.Projects.FirstOrDefault(a => a.ProjectId == projectId && !a.IsDeleted); 
                    if (project == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var users = db.UserProfiles
                        .Where(a => !a.UserToProjects
                        .Any(w => w.UserId == a.UserId
                        && w.ProjectId == projectId) && !a.IsDeleted).ToList();
                    var result = new List<InternalUserModel>();
                    users.ForEach(user =>
                    {
                        result.Add(new InternalUserModel(db, user));
                    });
                    return new InternalResultModel(StatusCodeEnum.Success, result);
                }
            } catch(Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// получаем список данных по всем проектам для пользователя
        /// </summary>
        public static InternalResultModel GetProjectsList(int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);

                    var allProj = db
                        .Projects.Where(a => !a.IsDeleted)
                        .ToList();

                    var projectForUser = allProj
                        .Where(a => 
                            a.UserToProjects
                                .Any(w => 
                                    w.UserId == userId))
                        .ToList();

                    var data = new List<InternalProjectModel>();
                    if (user.IsAdmin)
                    {
                        allProj.ForEach(a =>
                        {
                            data.Add(new InternalProjectModel(db, a));
                        });
                    }
                    else
                    {
                        projectForUser.ForEach(a =>
                        {
                            data.Add(new InternalProjectModel(db, a));
                        });
                    }

                    

                    return new InternalResultModel(StatusCodeEnum.Success, data);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// получаем подробную информацию по проекту
        /// </summary>
        public static InternalResultModel GetProjectInfoModel(int projectId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var project = db.Projects.FirstOrDefault(a => a.ProjectId == projectId && !a.IsDeleted);
                    if (project == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var result = new InternalProjectModel(db, project);
                    return new InternalResultModel(StatusCodeEnum.Success, result);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// создаем новый проект
        /// </summary>
        public static InternalResultModel CreateProject(InternalCreateProjectModel model) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == model.UserId);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var project = CommonWorkWithDbManager.CreateProjectModel(model.UserId,
                        model.ProjectDescription,
                        model.ProjectName);
                    if (project.StatusCode != StatusCodeEnum.Success) {
                        return project;
                    }

                    var projectToUser = CommonWorkWithDbManager.CreateUserToProjectModel(model.UserId,
                        ((Projects)project.Data).ProjectId, true);

                    if (projectToUser.StatusCode != StatusCodeEnum.Success) {
                        return projectToUser;
                    }
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// удаление проекта
        /// </summary>
        public static InternalResultModel DeleteProject(int projectId, int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var proj = db.Projects.FirstOrDefault(a => a.ProjectId == projectId);
                    if (proj == null)
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    if (proj.IsDeleted)
                        return new InternalResultModel(StatusCodeEnum.DataAlreadyDeleted);
                    proj.IsDeleted = true;
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// добавляем пользователя в проект
        /// </summary>
        public static InternalResultModel AddUserInProject(int userId, int projectId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var project = db.Projects.FirstOrDefault(a => a.ProjectId == projectId && !a.IsDeleted);
                    if (project == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    if (project.UserToProjects.Any(a => a.UserId == userId)) return null;

                    var result = CommonWorkWithDbManager.CreateUserToProjectModel(userId, projectId, false);
                    return result;
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// удаляем пользователя из проекта
        /// </summary>
        public static InternalResultModel DeleteUserFromProject(int userId, int projectId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var project = db.Projects.FirstOrDefault(a => a.ProjectId == projectId && !a.IsDeleted);
                    if (project == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var userInProject = db.UserToProjects.FirstOrDefault(a => a.UserId == userId && a.ProjectId == projectId);
                    if (userInProject == null) return new InternalResultModel(StatusCodeEnum.UserAlreadyInProject);

                    db.UserToProjects.Remove(userInProject);
                    db.SaveChanges();
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            } catch(Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}