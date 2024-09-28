using HDNXUdemyData.Model;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.ResponModel;

namespace HDNXUdemyServices.IServices
{
    public interface IMasterDataServices
    {
        Task<bool> CreateCategory(CategoryModel category);

        Task<bool> UpdateStatusCategory(long id, CategoryModel category);

        Task<bool> UpdateInformationCategory(long id, CategoryModel category);

        Task<List<CategoryModel>> GetListCategory();

        Task<bool> DeleteCategory(long id);

        Task<bool> CreateBanner(BannerModel model);

        Task<bool> UpdateStatusBanner(long id, BannerModel model);

        Task<bool> UpdateInformationBanner(long id, BannerModel model);

        Task<List<BannerModel>> GetListBanner();

        Task<bool> CreateInformationManualBanking(InformationManualBankingModel model);

        Task<bool> UpdateStatusInformationManualBanking(long id, InformationManualBankingModel model);

        Task<bool> UpdateInformationManualBanking(long id, InformationManualBankingModel model);

        Task<InformationManualBankingModel> GetInformationManualBanking();

        Task<bool> CreateSubCategory(SubCategoryModel model);

        Task<bool> UpdateStatusSubCategory(long id, SubCategoryModel model);

        Task<bool> UpdateInformationSubCategory(long id, SubCategoryModel model);

        Task<List<SubCategoryModel>> GetListSubCategory();

        Task<SubCategoryModel> GetSubCategory(long id);

        List<ListTypeFileUpload> GetListFileTypeUpload();

        Task<bool> CreateConfigSystem(SystemConfigModel model);

        Task<bool> UpdateStatusConfigSystem(long id, SystemConfigModel model);

        Task<bool> UpdateInformationConfigSystem(long id, SystemConfigModel model);

        Task<List<SystemConfigModel>> GetListConfigSystem();

        Task<SystemConfigModel> GetConfigSystem(long id);

        List<ListTypeFileUpload> GetListOfFileTypeUpload();
    }
}