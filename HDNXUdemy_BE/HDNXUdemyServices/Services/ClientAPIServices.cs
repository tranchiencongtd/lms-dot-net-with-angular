using HDNXUdemyData.IRepository;
using HDNXUdemyModel.Base;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyServices.IServices;

namespace HDNXUdemyServices.Services
{
    public class ClientAPIServices : ConfigClientAPIServices, IClientAPIServices
    {
        public ClientAPIServices(ISystemConfigRepository systemConfigRepository) : base(systemConfigRepository)
        {
        }

        public async Task<bool> GeneralTokenUploadServer(string email)
        {
            using (var client = await GetClientAPI())
            {
                var url = $"{ProjectConfig.BaseUrlAPI}/authentication/general-token/{email}";
                var response = await client.Get<RepositoryModel<ResponeLogin>>(url);
                if (response?.RetCode == ERetCode.Successfull)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}