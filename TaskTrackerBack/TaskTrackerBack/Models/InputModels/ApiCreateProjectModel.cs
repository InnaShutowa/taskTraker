using Newtonsoft.Json;

namespace TaskTrackerBack.Models.InputModels {
    public class ApiCreateProjectModel {
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
        [JsonProperty("project_name")]
        public string ProjectName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}