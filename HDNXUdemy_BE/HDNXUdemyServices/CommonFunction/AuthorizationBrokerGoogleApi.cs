using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using HDNXUdemyModel.Base;

namespace HDNXUdemyServices.CommonFunction
{
    public class AuthorizationBrokerGoogleApi : GoogleWebAuthorizationBroker
    {
        public static string? RedirectUri;

        public static async Task<UserCredential> AuthorizeAsync(
            string userEmail,
            CancellationToken taskCancellationToken,
            IDataStore? dataStore = null)
        {
            var scopes = new string[] { DriveService.Scope.Drive,
                               DriveService.Scope.DriveFile,};
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets()
                {
                    ClientId = ProjectConfig.ClientId,
                    ClientSecret = ProjectConfig.ClientSecret,
                },
                Scopes = scopes,
                DataStore = dataStore ?? new FileDataStore(Folder),
            };
            return await AuthorizeAsyncCore(initializer, userEmail,
                taskCancellationToken).ConfigureAwait(false);
        }

        private static async Task<UserCredential> AuthorizeAsyncCore(
            GoogleAuthorizationCodeFlow.Initializer initializer,
            string userEmail,
            CancellationToken taskCancellationToken)
        {
            var flow = new AuthorizationBrokerGoogleApiFlow(initializer);
            return await new AuthorizationCodeInstalledApp(flow,
                new LocalServerCodeReceiver())
                .AuthorizeAsync(userEmail, taskCancellationToken).ConfigureAwait(false);
        }

        public async Task<DriveService> InstanceGoogleDriveServices()
        {
            var userCredential = await AuthorizeAsync("tuan.nguyenminh2507@gmail.com", CancellationToken.None);
            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = "HDNX Upload file to google drive",
            });
        }
    }

    public class AuthorizationBrokerGoogleApiFlow : GoogleAuthorizationCodeFlow
    {
        public AuthorizationBrokerGoogleApiFlow(Initializer initializer)
            : base(initializer) { }

        public override AuthorizationCodeRequestUrl
                       CreateAuthorizationCodeRequest(string redirectUri)
        {
            return base.CreateAuthorizationCodeRequest(AuthorizationBrokerGoogleApi.RedirectUri);
        }
    }
}