using System.ComponentModel;

namespace TrackerLib.Enums {
    public enum StatusCodeEnum {
        Success = 400,
        InternalServerError = 500,
        [Description("Error! Apikey is empty.")]
        ApikeyIsEmpty = 501,
        [Description("Error! Apikey is wrong.")]
        ApikeyIsWrong = 502,
        [Description("Error! Input data is incorrect.")]
        InputDataIsWrong = 503,
        [Description("Error! Project already deleted.")]
        ProjectAlreadyDeleted = 504
    }
}