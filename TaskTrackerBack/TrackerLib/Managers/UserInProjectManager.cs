using System;
using System.Linq;
using TrackerLib.Enums;
using TrackerLib.Models;

namespace TrackerLib.Managers {
    public class UserInProjectManager {
        /// <summary>
        /// добавляем пользователя в проект
        /// </summary>
        public static InternalResultModel InviteUserInProject(int userId, int projectId) {
            try {
                using (var db = new TaskTrackerEntities()) {
                    var user = db.UserProfiles.FirstOrDefault(a => a.UserId == userId);
                    if (user == null) return null;
                    var project = db.Projects.FirstOrDefault(a => a.ProjectId == projectId && !a.IsDeleted);
                    if (project == null) return null;
                    if (project.UserToProjects.Any(a => a.UserId == userId)) return null;
                }
            }
            catch (Exception ex) {
                return new InternalResultModel(ex.Message, StatusCodeEnum.InternalServerError);
            }
            return null;
        }
        /// <summary>
        /// удаляем пользователя из проекта
        /// </summary>
        public static InternalResultModel DeleteUserFromProject(int userId,  int projectId) {
            return null;
        }
    }
}