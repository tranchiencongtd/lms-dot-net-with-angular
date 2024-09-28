using HDNXUdemyModel.Base;

namespace HDNXUdemyModel.ResponModel
{
    public class CheckoutSessionResponse
    {
        private string? _privateKey;
        public string? SessionId { get; set; }

        public string? PrivateKey { get => ProjectConfig.StripeSecretKey; set => _privateKey = value; }
    }
}