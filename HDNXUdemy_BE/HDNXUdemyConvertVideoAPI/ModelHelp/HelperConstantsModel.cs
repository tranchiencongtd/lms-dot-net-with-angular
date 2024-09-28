using HDNXUdemyModel.Base;

namespace HDNXUdemyConvertVideoAPI.ModelHelp
{
    /// <summary>
    /// HelperConstantsModel
    /// </summary>
    public class HelperConstantsModel
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// HelperConstantsModel
        /// </summary>
        /// <param name="config"></param>
        public HelperConstantsModel(IConfiguration config)
        {
            _configuration = config;
        }

        /// <summary>
        /// LoadConfig
        /// </summary>
        /// <param name="configuration"></param>
        public static void LoadConfig(IConfiguration configuration)
        {
            var appConfig = configuration.GetSection("AppConfig");
            var emailConfig = configuration.GetSection("MailSettings");
            var configCloud = configuration.GetSection("CloudinanySettings");
            var configFolder = configuration.GetSection("Folder");
            var configEmailTemplate = configuration.GetSection("EmailTemplate");
            var appSetting = configuration.GetSection("AppSettings");
            var configAWSCloud = configuration.GetSection("CloudServicesAWS");

            ProjectConfig.ConnectionString = appConfig.GetValue<string>("DefaultConnection");
            ProjectConfig.DefaultSchema = appConfig.GetValue<string>("Schema");
            ProjectConfig.AppName = appConfig.GetValue<string>("AppName");
            ProjectConfig.NumberFormat = appConfig.GetValue<string>("NumberFormat");
            ProjectConfig.DateFormat = appConfig.GetValue<string>("DateFormat");
            ProjectConfig.TimeFormat = appConfig.GetValue<string>("TimeFormat");
            ProjectConfig.SpecialCharacters = appConfig.GetValue<string>("SpecialCharacters");
            ProjectConfig.NumberPatternFormat = appConfig.GetValue<string>("NumberPatternFormat");
            ProjectConfig.DateTimeFormat = appConfig.GetValue<string>("DateTimeFormat");
            ProjectConfig.DateTimeSqlFormat = appConfig.GetValue<string>("DateTimeSQLFormat");
            ProjectConfig.DateExpSqlFormat = appConfig.GetValue<string>("DateExpSQLFormat");
            ProjectConfig.EmailFormat = appConfig.GetValue<string>("EmailFormat");
            ProjectConfig.SerialMmDdFormat = appConfig.GetValue<string>("SerialMmDdFormat");
            ProjectConfig.MaxDateExp = Convert.ToDateTime(appConfig.GetValue<string>("MaxDateExp"));
            ProjectConfig.SerialMmFormat = appConfig.GetValue<string>("SerialMmFormat");
            ProjectConfig.DefaultPassword = appConfig.GetValue<string>("DefaultPassword");
            ProjectConfig.NumberGeneratePassword = appConfig.GetValue<int>("NumberGeneratePassword");
            ProjectConfig.ApiKeyYoutubeApi = appConfig.GetValue<string>("ApiKeyYoutubeApi");
            ProjectConfig.NumberLastPost = appConfig.GetValue<int>("NumberLastPost");
            ProjectConfig.DiskBaseForVideo = appConfig.GetValue<string>("DiskBaseForVideo");

            ProjectConfig.SenderEmail = emailConfig.GetValue<string>("SenderEmail");
            ProjectConfig.DisplayName = emailConfig.GetValue<string>("DisplayName");
            ProjectConfig.Password = emailConfig.GetValue<string>("Password");
            ProjectConfig.HostName = emailConfig.GetValue<string>("Host");
            ProjectConfig.Port = emailConfig.GetValue<int>("Port");
            ProjectConfig.ClientId = emailConfig.GetValue<string>("ClientId");
            ProjectConfig.ClientSecret = emailConfig.GetValue<string>("ClientSecret");
            ProjectConfig.RefreshToken = emailConfig.GetValue<string>("RefreshToken");

            ProjectConfig.CloudName = configCloud.GetValue<string>("CloudName");
            ProjectConfig.APIKey = configCloud.GetValue<string>("APIKey");
            ProjectConfig.APISecret = configCloud.GetValue<string>("APISecret");

            ProjectConfig.EmailFolder = configFolder.GetValue<string>("EmailTemplate");
            ProjectConfig.StorageMainVideo = configFolder.GetValue<string>("StorageMainVideo");
            ProjectConfig.StorageStreamVideo = configFolder.GetValue<string>("StorageStreamVideo");
            ProjectConfig.UploadSoftWareAndFile = configFolder.GetValue<string>("UploadSoftWareAndFile");

            ProjectConfig.Secret = appSetting.GetValue<string>("Secret");
            ProjectConfig.ExpiresDate = appSetting.GetValue<int>("ExpiresDate");
            ProjectConfig.ExpiresLibrarySharebookConfirm = appSetting.GetValue<string>("ExpiresLibrarySharebookConfirm");
            ProjectConfig.TimeForExpireRegisterLink = appSetting.GetValue<int>("TimeForExpireRegisterLink");
            ProjectConfig.TimeForExpireLoginLink = appSetting.GetValue<int>("TimeForExpireLoginLink");

            ProjectConfig.AWSAccessKey = configAWSCloud.GetValue<string>("AWSAccessKey");
            ProjectConfig.AWSSecretKey = configAWSCloud.GetValue<string>("AWSSecretKey");
            ProjectConfig.AWSBucketName = configAWSCloud.GetValue<string>("AWSBucketName");
            ProjectConfig.Region = configAWSCloud.GetValue<string>("Region");
            ProjectConfig.Profile = configAWSCloud.GetValue<string>("Profile");
        }
    }
}