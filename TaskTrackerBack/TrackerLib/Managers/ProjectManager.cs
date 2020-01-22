using System;
using System.Collections.Generic;
using System.Linq;
using TrackerLib.Enums;
using TrackerLib.Models;
using TrackerLib.Models.InputModels;

namespace TrackerLib.Managers {
    public static class ProjectManager {
        /// <summary>
        /// получаем список данных по всем проектам для пользователя
        /// </summary>
        public static InternalResultModel GetProjectsList(int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);

                    var allProjects = db.UserToProjects
                        .Where(a => a.UserId == userId)
                        .OrderByDescending(a=>a.IsOwner)
                        .Select(a=>a.Projects)
                        .ToList();

                    var data = new List<InternalProjectModel>();

                    allProjects.ForEach(a => {
                        data.Add(new InternalProjectModel(db, a));
                    });

                    return new InternalResultModel(StatusCodeEnum.Success, data);
                }
            } catch (Exception ex) {
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
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// удаление проекта
        /// </summary>
        public static InternalResultModel DeleteProject(int projectId, int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var userToProject = db.UserToProjects.FirstOrDefault(a =>
                        a.UserId == userId && a.ProjectId == projectId && a.IsOwner);
                    if (userToProject == null)
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    if (userToProject.Projects.IsDeleted)
                        return new InternalResultModel(StatusCodeEnum.ProjectAlreadyDeleted);
                    userToProject.Projects.IsDeleted = true;
                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}