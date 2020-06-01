using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskTrackerBack.Models;
using TaskTrackerBack.Models.InputModels;
using TaskTrackerBack.Models.OutputModels;
using TrackerLib;
using TrackerLib.Enums;
using TrackerLib.Managers;
using TrackerLib.Models;
using TrackerLib.Models.InputModels;

namespace TaskTrackerBack.Managers {
    public static class ApiProjectManager {
        /// <summary>
        /// получаем списко пользователей, котоорых можно добавить в проект
        /// </summary>
        public static ResultModel GetUserToAdd(string apikey, int projectId) {
            try {
                if (string.IsNullOrEmpty(apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }

                var result = ProjectManager.GetUserToAddInProjectList(projectId);
                if (result.StatusCode != StatusCodeEnum.Success)
                    return new ResultModel(result.Error, result.StatusCode);
                if (result.Data == null)
                    return new ResultModel(StatusCodeEnum.InternalServerError);

                var data = new List<ApiOutputUserModel>();
                ((List<InternalUserModel>)result.Data).ForEach(a => data.Add(new ApiOutputUserModel(a)));

                return new ResultModel(data);
            } catch(Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// запрос на получение списка проектов
        /// </summary>
        public static ResultModel GetProjectsList(string apikey) {
            try {
                if (string.IsNullOrEmpty(apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }
                var projectsList = ProjectManager.GetProjectsList(((InternalUserModel)user.Data).UserId);
                if (projectsList.StatusCode != StatusCodeEnum.Success)
                    return new ResultModel(projectsList.Error, projectsList.StatusCode);
                if (projectsList.Data == null)
                    return new ResultModel(StatusCodeEnum.InternalServerError);

                var result = new List<ApiOutputProjectModel>();
                ((List<InternalProjectModel>)projectsList.Data).ForEach(a => result.Add(new ApiOutputProjectModel(a)));
                return new ResultModel(result);
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// запрос на получение подробной информации по одному проекту
        /// </summary>
        public static ResultModel GetProjectInfo(string apikey, int project_id) {
            try {
                if (string.IsNullOrEmpty(apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }
                var result = ProjectManager.GetProjectInfoModel(project_id);
                if (result.StatusCode != StatusCodeEnum.Success)
                    return new ResultModel(result.Error, result.StatusCode);

                if (result.Data == null) return new ResultModel(StatusCodeEnum.InternalServerError);

                return new ResultModel(new ApiOutputProjectModel((InternalProjectModel)result.Data));
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// создаем проект
        /// </summary>
        public static ResultModel CreateProject(ApiCreateProjectModel model) {
            try {
                if (string.IsNullOrEmpty(model.Apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                if (string.IsNullOrEmpty(model.ProjectName)) {
                    return new ResultModel(StatusCodeEnum.InputDataIsWrong);
                }
                var user = CommonManager.GetUserDataByApikey(model.Apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }
                var createdProject = ProjectManager.CreateProject(
                    new InternalCreateProjectModel(model.ProjectName,
                    model.Description,
                    ((InternalUserModel)user.Data).UserId));


                return new ResultModel(createdProject);
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// админ удаляет проект
        /// </summary>
        public static ResultModel DeleteProject(ApiGetProjectInfoModel model) {
            try {
                if (string.IsNullOrEmpty(model.Apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(model.Apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }
                if (!((InternalUserModel)user.Data).IsAdmin) return new ResultModel(StatusCodeEnum.NoRules);
                var result = ProjectManager.DeleteProject(model.ProjectId, ((InternalUserModel)user.Data).UserId);
                return new ResultModel(result);
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// добавляем пользователя в проект
        /// </summary>
        public static ResultModel AddUserToProject(ApiUserToProjectModel model) {
            try {
                using(var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(model.Apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var user = CommonManager.GetUserDataByApikey(model.Apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(user);
                    }
                    if (!((InternalUserModel)user.Data).IsAdmin) return new ResultModel(StatusCodeEnum.NoRules);
                    var result = ProjectManager.AddUserInProject(model.UserId, model.ProjectId);
                    return new ResultModel(result);
                }
            } catch(Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// удаляем пользователя из проекта
        /// </summary>
        public static ResultModel DeleteUserFromProject(ApiUserToProjectModel model) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(model.Apikey)) {
                        return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    }
                    var user = CommonManager.GetUserDataByApikey(model.Apikey);
                    if (user.StatusCode != StatusCodeEnum.Success) {
                        return new ResultModel(user);
                    }
                    if (!((InternalUserModel)user.Data).IsAdmin) return new ResultModel(StatusCodeEnum.NoRules);
                    var result = ProjectManager.DeleteUserFromProject(model.UserId, model.ProjectId);
                    return new ResultModel(result);
                }
            } catch(Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}