using HDNXUdemyData.IRepository;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.CommonFunction;
using HDNXUdemyServices.IServices;
using System.Net.Http.Headers;

namespace HDNXUdemyServices.Services
{
    public class ConfigClientAPIServices : IConfigClientAPIServices
    {
        private readonly ISystemConfigRepository _systemConfigRepository;

        public ConfigClientAPIServices(ISystemConfigRepository systemConfigRepository)
        {
            _systemConfigRepository = systemConfigRepository ?? throw new ProjectException(nameof(_systemConfigRepository));
        }

        public async Task<BaseClientAPIServices> GetClientAPI()
        {
            string token = (await _systemConfigRepository.GetObjectAsync(x => x.KeyConfig == KeyConfig.KeyTokenUpload)).Value ?? string.Empty;
            AuthenticationHeaderValue authenticationHeader;
            var header = new Dictionary<string, List<string>>();
            authenticationHeader = SetAuthenticationHeader(token);
            return new BaseClientAPIServices(header, authenticationHeader);
        }

        private AuthenticationHeaderValue SetAuthenticationHeader(string token)
        {
            return new AuthenticationHeaderValue("Bearer", token);
        }
    }
}