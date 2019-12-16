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
using TrackerLib.Models.InputModels;

namespace TaskTrackerBack.Managers {
    public static class ApiProjectManager {
        public static ResultModel GetProjectsList() {
            try {

                return new ResultModel();
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        public static ResultModel GetProjectInfo() {
            try {

                return new ResultModel();
            } catch (Exception ex) {
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
                var user = CommonManager.GetUserDataByApikey(model.Apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }
                var createdProject = ProjectManager.CreateProject(
                    new InternalCreateProjectModel(model.ProjectName, 
                    model.Description, 
                    ((UserProfiles)user.Data).UserId));


                return new ResultModel(createdProject);
            } catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        public static ResultModel DeleteProject() {
            try {

                return new ResultModel();
            } catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}