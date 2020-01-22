using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskTrackerBack.Models.InputModels;
using TaskTrackerBack.Models.OutputModels;
using TrackerLib.Enums;
using TrackerLib.Managers;
using TrackerLib.Models.InputModels;

namespace TaskTrackerBack.Managers {
    public static class ApiUserManager {
        /// <summary>
        /// cоздаем аккаунт для пользователя
        /// </summary>
        public static ResultModel CreateUserAcc(ApiCreateUserAccModel model) {
            try {
                if (string.IsNullOrEmpty(model.Apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(model.Apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }

                var result = UserManager.CreateUserAcc(
                    new InternalCreateUserAccModel(model.Phone,
                    model.Email,
                    model.FirstName,
                    model.LastName,
                    model.BirthDate,
                    model.IsAdmin));

                return new ResultModel(result);
            } catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        public static ResultModel GetUsersList() {
            try {

            } catch(Exception ex) {

            }
            return null;
        }

        public static ResultModel GetUserInfo(string apikey, int userId) {
            try {

            } catch(Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
            return null;
        }
    }
}