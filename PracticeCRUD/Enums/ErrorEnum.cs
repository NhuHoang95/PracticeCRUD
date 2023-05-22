
using System.ComponentModel;

namespace PracticeCRUD.Enums
{
    public enum ErrorEnum
    {
        [Description("Success")]
        Success = 1,
        [Description("Failed")]
        Failed = 2,
        [Description("DataNotFound")]
        DataNotFound = 3,
        [Description("Error occur while updating")]
        UpdateFailed = 5,
        [Description("Request is null")]
        RequestNull = 6,
    }
}
