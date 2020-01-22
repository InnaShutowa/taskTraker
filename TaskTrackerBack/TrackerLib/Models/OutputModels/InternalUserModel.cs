using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLib.Models {
    /// <summary>
    /// модель данных по пользователю
    /// </summary>
    public class InternalUserModel {
        public InternalUserModel() { }

        public InternalUserModel(TaskTrackerEntities db, UserProfiles data) {
            if (data == null) return;
            UserId = data.UserId;
            Email = data.Email;
            Phone = data.Phone;
            BirthDate = data.BirthDate;
            IsAdmin = data.IsAdmin;
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
