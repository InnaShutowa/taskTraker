using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLib.Models.InputModels {
    /// <summary>
    /// модель для регистрации нового пользователя
    /// </summary>
    public class InternalCreateUserAccModel {
        public InternalCreateUserAccModel() { }
        public InternalCreateUserAccModel(string phone,
            string email,
            string firstName,
            string lastName,
            DateTime? birthDate,
            bool isAdmin) {
            Phone = phone;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            IsAdmin = isAdmin;
            BirthDate = birthDate ?? new DateTime();
        }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
