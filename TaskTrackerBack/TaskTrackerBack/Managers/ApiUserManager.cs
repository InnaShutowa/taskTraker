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

namespace TaskTrackerBack.Managers {
    public static class ApiUserManager {
        /// <summary>
        /// удаляем аккаунт пользователя
        /// </summary>
        public static ResultModel DeleteUserAcc(ApiGetUserInfoByIdModel model) {
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
                var result = UserManager.DeleteUserInfo(model.UserId, ((InternalUserModel)user.Data).UserId);
                return new ResultModel(result);
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// cоздаем аккаунт для пользователя
        /// </summary>
        public static ResultModel CreateUserAcc(ApiCreateUserAccModel model) {
            try {
                if (string.IsNullOrEmpty(model.Apikey))
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                
                var user = CommonManager.GetUserDataByApikey(model.Apikey);
                if (user.StatusCode != StatusCodeEnum.Success) 
                    return new ResultModel(user);
                
                if (!((InternalUserModel)user.Data).IsAdmin)
                    return new ResultModel(StatusCodeEnum.NoRules);

                var result = UserManager.CreateUserAcc(
                    new InternalCreateUserAccModel(model.Phone,
                    model.Email,
                    model.FirstName,
                    model.LastName,
                    model.BirthDate,
                    model.IsAdmin));

                return new ResultModel(result);
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// получаем список активных пользователей
        /// </summary>
        public static ResultModel GetUsersList(string apikey) {
            try {
                if (string.IsNullOrEmpty(apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(apikey);
                if (user.StatusCode != StatusCodeEnum.Success) {
                    return new ResultModel(user);
                }
                var result = UserManager.GetUsersList();
                if (result.StatusCode != StatusCodeEnum.Success) return new ResultModel(result);

                if (result.Data == null) return new ResultModel(StatusCodeEnum.InternalServerError);
                var data = new List<ApiOutputUserModel>();
                ((List<InternalUserModel>)result.Data).ForEach(a => data.Add(new ApiOutputUserModel(a)));
                return new ResultModel(data);
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// получаем информацию по пользователю по апикею (инфа для личного кабинета)
        /// </summary>
        public static ResultModel GetUserInfoByApikey(string apikey) {
            try {
                if (string.IsNullOrEmpty(apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(apikey);
                if (user.StatusCode != StatusCodeEnum.Success) return new ResultModel(user);

                if (user.Data == null) return new ResultModel(StatusCodeEnum.InternalServerError);

                return new ResultModel(new ApiOutputUserModel((InternalUserModel)user.Data));
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// админ получает инфу по пользователю
        /// </summary>
        public static ResultModel GetUserInfoById(string apikey, int userId) {
            try {
                if (string.IsNullOrEmpty(apikey)) {
                    return new ResultModel(StatusCodeEnum.ApikeyIsEmpty);
                }
                var user = CommonManager.GetUserDataByApikey(apikey);
                if (user.StatusCode != StatusCodeEnum.Success) return new ResultModel(user);

                var userInfo = UserManager.GetUserInfo(userId);
                if (userInfo.StatusCode != StatusCodeEnum.Success) return new ResultModel(userInfo);
                if (userInfo.Data == null) return new ResultModel(StatusCodeEnum.InternalServerError);
                return new ResultModel(new ApiOutputUserModel((InternalUserModel)userInfo.Data));
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        public static ResultModel AuthUser(string login, string pass) {
            try {
                if (string.IsNullOrEmpty(login)
                    || string.IsNullOrEmpty(pass))
                    return new ResultModel(StatusCodeEnum.InputDataIsWrong);

                using (var db = new TaskTrackerEntities()) {

                    var user = db.UserAuthData
                        .FirstOrDefault(a => a.PasswordHash == pass && a.UserProfiles.Email == login);
                    if (user == null) return new ResultModel(StatusCodeEnum.InputDataIsWrong);
                    return new ResultModel(new ApiOutputAuthUser()
                    {
                        UserId = user.UserId,
                        Apikey = user.Apikey,
                        IsAdmin = user.UserProfiles.IsAdmin
                    });
                }
            }
            catch (Exception ex) {
                return new ResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

    }
}