using AutoMapper;
using HDNXUdemyData.Entities;
using HDNXUdemyData.IRepository;
using HDNXUdemyData.Model;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.Model;
using HDNXUdemyModel.ResponModel;
using HDNXUdemyModel.SystemExceptions;
using HDNXUdemyServices.CommonFunction;
using HDNXUdemyServices.IServices;

namespace HDNXUdemyServices.Services
{
    public class MasterDataServices : IMasterDataServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBannerRepository _bannerRepository;
        private readonly IInformationManualBankingRepository _informationManualBankingRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IUploadDataToCloud _uploadDataToCloud;
        private readonly ICourseRepository _courseRepository;
        private readonly ISystemConfigRepository _systemConfigRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// MasterDataServices
        /// </summary>
        /// <param name="categoryRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="bannerRepository"></param>
        /// <exception cref="ProjectException"></exception>
        public MasterDataServices(ICategoryRepository categoryRepository, IMapper mapper, IBannerRepository bannerRepository, IUploadDataToCloud uploadDataToCloud,
            IInformationManualBankingRepository informationManualBankingRepository, ISubCategoryRepository subCategoryRepository,
            ICourseRepository courseRepository, ISystemConfigRepository systemConfigRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ProjectException(nameof(_categoryRepository));
            _mapper = mapper ?? throw new ProjectException(nameof(_mapper));
            _bannerRepository = bannerRepository ?? throw new ProjectException(nameof(_bannerRepository));
            _informationManualBankingRepository = informationManualBankingRepository ?? throw new ProjectException(nameof(_informationManualBankingRepository));
            _subCategoryRepository = subCategoryRepository ?? throw new ProjectException(nameof(_subCategoryRepository));
            _uploadDataToCloud = uploadDataToCloud ?? throw new ProjectException(nameof(_uploadDataToCloud));
            _courseRepository = courseRepository ?? throw new ProjectException(nameof(_courseRepository));
            _systemConfigRepository = systemConfigRepository ?? throw new ProjectException(nameof(_systemConfigRepository));
        }

        public async Task<bool> CreateCategory(CategoryModel category)
        {
            var dataInsert = _mapper.Map<CategoryEntities>(category);
            return await _categoryRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusCategory(long id, CategoryModel category)
        {
            var getData = await _categoryRepository.GetByIdAsync(id) ?? new CategoryEntities();
            getData.Status = category.Status;
            return await _categoryRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationCategory(long id, CategoryModel category)
        {
            var getData = await _categoryRepository.GetByIdAsync(id) ?? new CategoryEntities();
            getData.Status = category.Status;
            getData.Name = category.Name;
            getData.PictureUrl = category.PictureUrl;
            getData.PublicId = category.PublicId;
            return await _categoryRepository.UpdateAsync(getData);
        }

        public async Task<bool> DeleteCategory(long id)
        {
            return await _categoryRepository.DeleteByKey(id);
        }

        public async Task<List<CategoryModel>> GetListCategory()
        {
            var getData = await _categoryRepository.GetAllAsync();
            var resultValue = _mapper.Map<List<CategoryModel>>(getData);
            foreach (var item in resultValue)
            {
                item.NumberCourse = (await _courseRepository.GetAsync(x => x.IdCategory == item.Id)).Count();
            }
            return resultValue;
        }

        public async Task<bool> CreateBanner(BannerModel model)
        {
            var dataInsert = _mapper.Map<BannerEntities>(model);
            return await _bannerRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusBanner(long id, BannerModel model)
        {
            var getData = await _bannerRepository.GetByIdAsync(id) ?? new BannerEntities();
            getData.Status = model.Status;
            return await _bannerRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationBanner(long id, BannerModel model)
        {
            var getData = await _bannerRepository.GetByIdAsync(id) ?? new BannerEntities();
            getData.Status = model.Status;
            getData.ContentBanner = model.ContentBanner;
            getData.UrlPicture = model.UrlPicture;
            getData.PublicId = model.PublicId;
            getData.IsActive = model.IsActive;
            return await _bannerRepository.UpdateAsync(getData);
        }

        public async Task<List<BannerModel>> GetListBanner()
        {
            var getData = await _bannerRepository.GetAllAsync();
            return _mapper.Map<List<BannerModel>>(getData);
        }

        public async Task<bool> CreateInformationManualBanking(InformationManualBankingModel model)
        {
            var dataInsert = _mapper.Map<InformationManualBankingEntities>(model);
            return await _informationManualBankingRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusInformationManualBanking(long id, InformationManualBankingModel model)
        {
            var getData = await _informationManualBankingRepository.GetByIdAsync(id) ?? new InformationManualBankingEntities();
            getData.Status = model.Status;
            return await _informationManualBankingRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationManualBanking(long id, InformationManualBankingModel model)
        {
            var getData = await _informationManualBankingRepository.GetByIdAsync(id) ?? new InformationManualBankingEntities();
            getData.Status = model.Status;
            getData.NumberBanking = model.NumberBanking;
            getData.AccountName = model.AccountName;
            getData.NameBanking = model.NameBanking;
            getData.Notes = model.Notes;
            return await _informationManualBankingRepository.UpdateAsync(getData);
        }

        public async Task<InformationManualBankingModel> GetInformationManualBanking()
        {
            var getData = await _informationManualBankingRepository.GetAllAsync();
            return _mapper.Map<InformationManualBankingModel>(getData.FirstOrDefault());
        }

        public async Task<bool> CreateSubCategory(SubCategoryModel model)
        {
            var dataInsert = _mapper.Map<SubCategoryEntities>(model);
            return await _subCategoryRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusSubCategory(long id, SubCategoryModel model)
        {
            var getData = await _subCategoryRepository.GetByIdAsync(id) ?? new SubCategoryEntities();
            getData.Status = model.Status;
            return await _subCategoryRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationSubCategory(long id, SubCategoryModel model)
        {
            var getData = await _subCategoryRepository.GetByIdAsync(id) ?? new SubCategoryEntities();
            getData.Status = model.Status;
            getData.Name = model.Name;
            getData.IdCategory = model.IdCategory;
            return await _subCategoryRepository.UpdateAsync(getData);
        }

        public async Task<List<SubCategoryModel>> GetListSubCategory()
        {
            var getData = await _subCategoryRepository.GetAllAsync();
            return _mapper.Map<List<SubCategoryModel>>(getData);
        }

        public async Task<SubCategoryModel> GetSubCategory(long id)
        {
            var getData = await _subCategoryRepository.GetByIdAsync(id);
            return _mapper.Map<SubCategoryModel>(getData);
        }

        public async Task<bool> CreateConfigSystem(SystemConfigModel model)
        {
            var dataInsert = _mapper.Map<SystemConfigEntities>(model);
            return await _systemConfigRepository.AddAsync(dataInsert);
        }

        public async Task<bool> UpdateStatusConfigSystem(long id, SystemConfigModel model)
        {
            var getData = await _systemConfigRepository.GetByIdAsync(id) ?? new SystemConfigEntities();
            getData.Status = model.Status;
            return await _systemConfigRepository.UpdateStatusAsync(getData);
        }

        public async Task<bool> UpdateInformationConfigSystem(long id, SystemConfigModel model)
        {
            var getData = await _systemConfigRepository.GetByIdAsync(id) ?? new SystemConfigEntities();
            getData.Status = model.Status;
            getData.Value = model.Value;
            return await _systemConfigRepository.UpdateAsync(getData);
        }

        public async Task<List<SystemConfigModel>> GetListConfigSystem()
        {
            var getData = await _systemConfigRepository.GetAllAsync();
            return _mapper.Map<List<SystemConfigModel>>(getData);
        }

        public async Task<SystemConfigModel> GetConfigSystem(long id)
        {
            var getData = await _systemConfigRepository.GetByIdAsync(id);
            return _mapper.Map<SystemConfigModel>(getData);
        }

        public List<ListTypeFileUpload> GetListFileTypeUpload()
        {
            var getData = ProjectStringExtensions.GetEnumList<EFileType>();
            return getData;
        }

        public List<ListTypeFileUpload> GetListOfFileTypeUpload()
        {
            var getData = ProjectStringExtensions.GetEnumList<EFileTypeUpload>();
            return getData;
        }
    }
}