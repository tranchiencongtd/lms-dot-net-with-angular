using HDNXUdemyModel.Constant;

namespace HDNXUdemyModel.Base
{
    public class RepositoryModel<T>
    {
        public string? PartnerCode { get; set; }

        private int _code = 0;

        public ERetCode RetCode
        {
            get { return (ERetCode)_code; }
            set { _code = (int)value; }
        }

        public T? Data { get; set; }

        public int StatusCode { get; set; }

        public string? SystemMessage { get; set; }

        public RepositoryModel(ERetCode retCode, T value)
        {
            this.RetCode = retCode;
            this.Data = value;
        }

        public RepositoryModel()
        {
            PartnerCode = "0";
        }
    }
}