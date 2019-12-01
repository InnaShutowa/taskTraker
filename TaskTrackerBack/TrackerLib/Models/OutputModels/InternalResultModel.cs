using System.Windows.Forms.VisualStyles;
using TrackerLib.Enums;

namespace TrackerLib.Models {
    /// <summary>
    /// внутренняя результирующая модель для всех запросов
    /// </summary>
    public class InternalResultModel {
        public InternalResultModel() { }

        public InternalResultModel(string error, StatusCodeEnum statusCode) {
            Error = error;
            StatusCode = statusCode;
        }

        public InternalResultModel(StatusCodeEnum statusCode, object data) {
            StatusCode = statusCode;
            Data = data;
        }

        public InternalResultModel(StatusCodeEnum statusCode) {
            StatusCode = statusCode;
        }

        public string Error { get; set; }
        public object Data { get; set; }

        public StatusCodeEnum StatusCode { get; set; }
    }
}