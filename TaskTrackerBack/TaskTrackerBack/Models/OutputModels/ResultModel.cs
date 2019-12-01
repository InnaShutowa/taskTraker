using EnumsNET;
using Newtonsoft.Json;
using TrackerLib.Enums;
using TrackerLib.Models;

namespace TaskTrackerBack.Models.OutputModels {
    /// <summary>
    /// модель результата всех операций
    /// </summary>
    public class ResultModel {
        public ResultModel() { }

        public ResultModel(string error, StatusCodeEnum statusCode) {
            StatusCode = statusCode;
            Error = error;
        }

        public ResultModel(object data) {
            Data = data;
            StatusCode = StatusCodeEnum.Success;
        }

        public ResultModel(StatusCodeEnum statusCode) {
            StatusCode = statusCode;
            Error = statusCode.AsString(EnumFormat.Description);
        }

        public ResultModel(InternalResultModel result) {
            switch (result.StatusCode) {
                case StatusCodeEnum.Success: {
                        StatusCode = StatusCodeEnum.Success;
                        Data = result.Data;
                        break;
                    }
                case StatusCodeEnum.InternalServerError: {
                        StatusCode = result.StatusCode;
                        Error = result.Error;
                        break;
                    }
                default: {
                        StatusCode = result.StatusCode;
                        Error = result.StatusCode.AsString(EnumFormat.Description);
                        break;
                    }
            }

        }

        [JsonProperty("status_code")]
        public StatusCodeEnum StatusCode { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}