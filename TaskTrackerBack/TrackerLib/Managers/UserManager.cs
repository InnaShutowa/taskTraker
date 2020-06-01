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
                if (string.IsNullOrEmpty(model.Email) ||
                       string.IsNullOrEmpty(model.Phone) ||
                       string.IsNullOrEmpty(model.FirstName) ||
                       string.IsNullOrEmpty(model.LastName) ||
                       !model.Email.Contains('.') ||
                       !model.Email.Contains('@') ||
                       model.Email.Length < 4 ||
                       model.Email.IndexOfAny(new[] { '.', '@'}) == 0 ||
                       model.Email.IndexOfAny(new[] { '.', '@' }) == model.Email.Length - 1 ||
                       (model.Phone.Length !=11 && model.Phone.Length != 12) ||
                       (model.Phone[0] != '8' && model.Phone[0] != '+')
                       )
                    return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                using (var db = new TaskTrackerEntities()) {

                    var user = db.UserProfiles.FirstOrDefault(a => a.Email == model.Email ||
                                                        a.Phone == model.Phone);
                    if (user != null) return new InternalResultModel(StatusCodeEnum.EmailAlreadyUsed);

                    var apikey = CommonManager.GetRandomString(15);
                    var password = CommonManager.GetRandomString(9);
                    var md5 = MD5.Create();

                    byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                    var sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++) {
                        sBuilder.Append(data[i].ToString());
                    }

                    var hash = sBuilder.ToString();
                    CommonWorkWithDbManager.CreateUserModel(model, apikey, hash);

                    return new InternalResultModel(StatusCodeEnum.Success, password);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

        /// <summary>
        /// авторизация пользователя
        /// </summary>
        public static InternalResultModel AuthUser() {
            try {

            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
            return null;
        }
        /// <summary>
        /// получаем информацию по всем активным пользователям
        /// </summary>
        public static InternalResultModel GetUsersList() {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var users = db.UserProfiles.Where(a => !a.IsDeleted).ToList();
                    var result = new List<InternalUserModel>();
                    users.ForEach(user => result.Add(new InternalUserModel(db, user)));

                    return new InternalResultModel(StatusCodeEnum.Success, result);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// получаем информацию о пользователе по id
        /// </summary>
        public static InternalResultModel GetUserInfo(int userId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId && !a.IsDeleted);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    var userModel = new InternalUserModel(db, user);
                    return new InternalResultModel(StatusCodeEnum.Success, userModel);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
        /// <summary>
        /// удаляем аккаунт пользователя
        /// </summary>
        public static InternalResultModel DeleteUserInfo(int userId, int adminId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId);
                    if (user == null) return new InternalResultModel(StatusCodeEnum.InputDataIsWrong);
                    if (user.IsDeleted) return new InternalResultModel(StatusCodeEnum.DataAlreadyDeleted);
                    user.IsDeleted = true;

                    var tasks = db.Tasks
                        .Where(a => a.UserId == user.UserId
                    && a.TaskStatus != (int)TaskStatusEnum.Ready).ToList();

                    tasks.ForEach(task =>
                    {
                        task.UserId = adminId;
                    });
                    db.SaveChanges();

                    return new InternalResultModel(StatusCodeEnum.Success);
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }
    }
}
