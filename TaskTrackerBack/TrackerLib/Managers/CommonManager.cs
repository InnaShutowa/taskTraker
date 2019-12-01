using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLib.Enums;
using TrackerLib.Models;

namespace TrackerLib.Managers {
    public static class CommonManager {
        /// <summary>
        /// получаем пользовательские данные
        /// </summary>
        public static InternalResultModel GetUserDataByApikey(string apikey) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    if (string.IsNullOrEmpty(apikey))
                        return new InternalResultModel(StatusCodeEnum.ApikeyIsEmpty);
                    var userAuthData = db.UserAuthData
                        .FirstOrDefault(a => a.Apikey == apikey
                                             && !a.UserProfiles.IsDeleted);

                    var userData = userAuthData?.UserProfiles;
                    if (userData == null)
                        return new InternalResultModel(StatusCodeEnum.ApikeyIsWrong);
                    var userModel = new InternalUserModel(db, userData);
                    return new InternalResultModel(StatusCodeEnum.Success, userModel);
                }
            } catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
        }

    }
}
