using System.ComponentModel;

namespace HDNXUdemyModel.Constant
{
    public enum ERoles
    {
        [Description("Admin")]
        Admin,

        [Description("User")]
        User,

        [Description("Student")]
        Student,

        [Description("Teacher")]
        Teacher
    }

    public enum ETypeOfCourse
    {
        [Description("Khoá học")]
        Normal,

        [Description("TrainningWithTeacher")]
        TrainningWithTeacher
    }

    public enum ETypeOfStatusOrder
    {
        [Description("Request")]
        Request,

        [Description("Payment")]
        Payment,

        [Description("Pending-Payment")]
        Pending,

        [Description("Reject")]
        Reject
    }

    public enum ERetCode
    {
        Successfull,

        BadRequest,

        SystemError,

        LoginSuccess,

        LoginError,

        ExitAccount,

        ErrorCookie,

        PasswordNotSame,

        NoExitData,

        ConfictData
    }

    public enum EFilter
    {
        All = -1
    }

    public enum ERepositoryStatus
    {
        Success = 200,

        CreateSuccess = 201,

        NoContent = 204,

        Error = 400,

        InternalError = 500,

        BadRequest = 404,

        Precondition = 412,

        NoCookie = 999,

        Confict = 409
    }

    public enum EStatus
    {
        Active,
        Inactive,
        Comfirm,
        UnComfirm
    }

    public enum EStatusProduct
    {
        Avalible,
        Taken,
        Swapped
    }

    public enum ETypeAction
    {
        Login,
        Register,
        Get,
        Create,
        Update,
        Delete,
        Middleware
    }

    public enum HttpErrorCode
    {
        Success = 200,

        CreateSuccess = 201,

        NoContent = 204,

        Error = 400,

        InternalError = 500,

        BadRequest = 404,

        Precondition = 412,

        NoCookie = 999
    }

    public enum ETypeLogin
    {
        Google,
        Facebook,
        Email
    }

    public enum EFileType
    {
        [Description("Phần mềm kỹ thuật")]
        SoftWareTech,

        [Description("File 2D_3D")]
        FilePrint2D3D,

        [Description("Tài liệu khuôn thước - TLM")]
        FileTLM,

        [Description("Tài liệu gia công lập trình - TLCNC")]
        FileTLCNC
    }

    public enum EFileTypeUpload
    {
        [Description("bi bi-filetype-pdf")]
        Pdf,

        [Description("bi bi-filetype-psd")]
        Psd,

        [Description("bi bi-filetype-mp4")]
        Mp4,

        [Description("bi bi-filetype-gif")]
        Gif,

        [Description("bi bi-filetype-svg")]
        Svg,

        [Description("bi bi-filetype-scss")]
        Scss,

        [Description("bi bi-filetype-docx")]
        Docx,

        [Description("bi bi-filetype-exe")]
        Exe,

        [Description("bi bi-filetype-heic")]
        Heic,

        [Description("bi bi-filetype-jpg")]
        Jpg,

        [Description("bi bi-filetype-js")]
        Js,

        [Description("bi bi-filetype-json")]
        Json,

        [Description("bi bi-filetype-jsx")]
        Jsx,

        [Description("bi bi-filetype-ai")]
        AI,

        [Description("bi bi-filetype-key")]
        Another,
    }

    public enum ProcessVideo
    {
        [Description("Open")]
        Open,

        [Description("Process")]
        Process,

        [Description("Finish")]
        Finish,

        [Description("Public")]
        Public,

        [Description("Inactive")]
        Inactive
    }

    public enum TypeNotification
    {
        [Description("CommentOnCourse")]
        CommentOnCourse,

        [Description("TagOnCommentOnCourse")]
        TagOnCommentOnCourse,

        [Description("UpdateOnCourse")]
        UpdateOnCourse,

        [Description("DiscountOnCourse")]
        DiscountOnCourse,

        [Description("PromotionOnCourse")]
        PromotionOnCourse,

        [Description("UpgradeSystemOnCourse")]
        UpgradeSystemOnCourse
    }
}