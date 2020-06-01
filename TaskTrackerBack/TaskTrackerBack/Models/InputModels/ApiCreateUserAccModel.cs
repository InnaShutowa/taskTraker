using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTrackerBack.Models.InputModels {
    public class ApiOuthUser {
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
    public class ApiGetUserInfoByIdModel {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
    }
    public class ApiCreateUserAccModel {
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("birth_date")]
        public DateTime BirthDate { get; set; }
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
    }
}