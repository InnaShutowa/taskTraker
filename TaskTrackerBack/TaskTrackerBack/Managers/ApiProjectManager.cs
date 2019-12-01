using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskTrackerBack.Models;
using TaskTrackerBack.Models.InputModels;
using TaskTrackerBack.Models.OutputModels;
using TrackerLib.Enums;
using TrackerLib.Managers;

namespace TaskTrackerBack.Managers {
    public class ApiProjectManager {
        public ResultModel GetProjectsList() {
            try {

                return new ResultModel();
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        public ResultModel GetProjectInfo() {
            try {

                return new ResultModel();
            } catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// создаем проект
        /// </summary>
        public ResultModel CreateProject(ApiCreateProjectModel model) {
            try {
                if (string.IsNullOrEmpty(model.Apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(model.Apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }
               // var createdProject =
                return new ResultModel();
            } catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        public ResultModel DeleteProject() {
            try {

                return new ResultModel();
            } catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}