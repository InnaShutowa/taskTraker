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
        [Description("Error! Data already deleted.")]
        DataAlreadyDeleted = 504,
        [Description("Error! Email already used.")]
        EmailAlreadyUsed = 505,
        [Description("Error! There aren't enought rules.")]
        NoRules = 506,
        [Description("Error! User already in project.")]
        UserAlreadyInProject = 507
    }
}