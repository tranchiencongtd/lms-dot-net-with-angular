using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.Model
{
    public class InformationManualBankingModel : BaseModel
    {
        public string? NameBanking { get; set; }

        public string? AccountName { get; set; }

        public string? NumberBanking { get; set; }

        public string? Notes { get; set; }
    }
}