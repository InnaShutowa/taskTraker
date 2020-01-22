using ServerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TrackerLib.Enums;
using TrackerLib.Models;
using TrackerLib.Models.InputModels;

namespace TrackerLib.Managers {
    public static class UserManager {
        /// <summary>
        /// регистрация пользователя
        /// </summary>
        public static InternalResultModel CreateUserAcc(InternalCreateUserAccModel model) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(model.Email) ||
                        string.IsNullOrEmpty(model.Phone) ||
                        string.IsNullOrEmpty(model.FirstName) ||
                        string.IsNullOrEmpty(model.LastName)
                     )
                        return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);

                    var user = db.UserProfiles.FirstOrDefault(a => a.Email == model.Email);
                    if (user != null) return new InternalResultModel(StatusCodeEnum.EmailAlreadyUsed);

                    var apikey = CommonManager.GetRandomString(15);
                    var password = CommonManager.GetRandomString(9);
                    var md5 = MD5.Create();

                    byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                   
                    var sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++) {
                        sBuilder.Append(data[i].ToString("x2"));
                    }

                    var hash = sBuilder.ToString();
                    CommonWorkWithDbManager.CreateUserModel(model, apikey, hash);

                    return new InternalResultModel(StatusCodeEnum.Success, password);
                }
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }


        /// <summary>
        /// авторизация пользователя
        /// </summary>
        public static InternalResultModel AuthUser() {
            try {

            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
            return null;
        }

        public static InternalResultModel GetUsersList() {
            try {
                using (var db = new TaskTrackerEntities()) {
                    return null;
                }
            } catch(Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// получаем информацию о пользователе по id
        /// </summary>
        public static InternalResultModel GetUserInfo(int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var userModel = new InternalUserModel(db, user);
                    return new InternalResultModel(StatusCodeEnum.Success, userModel);
                }
            } catch(Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}
